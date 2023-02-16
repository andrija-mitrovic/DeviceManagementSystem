using Application.Common.DTOs;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Devices.Queries.GetDevicesWithPagination
{
    internal sealed class GetDevicesWithPaginationQueryHandler : IRequestHandler<GetDevicesWithPaginationQuery, PagedList<DeviceDTO>>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDevicesWithPaginationQueryHandler> _logger;

        public GetDevicesWithPaginationQueryHandler(
            IDeviceRepository deviceRepository, 
            IMapper mapper, 
            ILogger<GetDevicesWithPaginationQueryHandler> logger)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PagedList<DeviceDTO>> Handle(GetDevicesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving devices by parameters...");

            var devicesWithMetaData = await _deviceRepository.GetDevicesByParametersAsync(request.DeviceParameters, true, cancellationToken);

            return _mapper.Map<PagedList<DeviceDTO>>(devicesWithMetaData);
        }
    }
}
