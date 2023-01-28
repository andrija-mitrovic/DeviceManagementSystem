using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DeviceTypes.Commands.CreateDeviceType
{
    internal sealed class CreateDeviceTypeCommandHandler : IRequestHandler<CreateDeviceTypeCommand, int>
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IDeviceTypePropertyRepository _deviceTypePropertyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDeviceTypeCommandHandler> _logger;

        public CreateDeviceTypeCommandHandler(
            IDeviceTypeRepository deviceTypeRepository, 
            IDeviceTypePropertyRepository deviceTypePropertyRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<CreateDeviceTypeCommandHandler> logger)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _deviceTypePropertyRepository = deviceTypePropertyRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateDeviceTypeCommand request, CancellationToken cancellationToken)
        {
            var deviceType = await _deviceTypeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (deviceType is null)
            {
                _logger.LogInformation($"Creating {nameof(DeviceType)}...");

                deviceType = _mapper.Map<DeviceType>(request);

                await _deviceTypeRepository.AddAsync(deviceType, cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(nameof(DeviceType) + " is successfully created.");

                return deviceType.Id;
            }
            else
            {
                _logger.LogInformation($"Updating {nameof(Device)}...");

                _mapper.Map(request, deviceType);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(nameof(DeviceType) + " is successfully updated.");

                return deviceType.Id;
            }
        }
    }
}
