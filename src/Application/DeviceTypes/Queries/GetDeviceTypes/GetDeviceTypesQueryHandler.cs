using Application.Common.DTOs;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DeviceTypes.Queries.GetDeviceTypes
{
    internal sealed class GetDeviceTypesQueryHandler : IRequestHandler<GetDeviceTypesQuery, List<DeviceTypeDTO>>
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetDeviceTypesQueryHandler> _logger;

        public GetDeviceTypesQueryHandler(
            IDeviceTypeRepository deviceTypeRepository, 
            IMapper mapper,
            ILogger<GetDeviceTypesQueryHandler> logger)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<DeviceTypeDTO>> Handle(GetDeviceTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving device types...");

            var deviceTypes = await _deviceTypeRepository.GetAllAsync(true, cancellationToken);

            return _mapper.Map<List<DeviceTypeDTO>>(deviceTypes);
        }
    }
}
