using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Devices.Queries.GetDevices
{
    internal sealed class GetDevicesQueryHandler : IRequestHandler<GetDevicesQuery, List<DeviceDTO>>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDevicesQueryHandler> _logger;

        public GetDevicesQueryHandler(
            IDeviceRepository deviceRepository, 
            IMapper mapper, 
            ILogger<GetDevicesQueryHandler> logger)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<DeviceDTO>> Handle(GetDevicesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving devices...");

            var devices = await _deviceRepository.GetAllAsync(true, cancellationToken);

            return _mapper.Map<List<DeviceDTO>>(devices);
        }
    }
}
