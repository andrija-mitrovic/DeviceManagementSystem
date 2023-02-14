using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DeviceTypes.Commands.CreateUpdateDeviceType
{
    internal sealed class CreateUpdateDeviceTypeCommandHandler : IRequestHandler<CreateUpdateDeviceTypeCommand, int>
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUpdateDeviceTypeCommandHandler> _logger;

        public CreateUpdateDeviceTypeCommandHandler(
            IDeviceTypeRepository deviceTypeRepository, 
            IUnitOfWork unitOfWork, 
            IMapper mapper, 
            ILogger<CreateUpdateDeviceTypeCommandHandler> logger)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateUpdateDeviceTypeCommand request, CancellationToken cancellationToken)
        {
            var deviceType = await _deviceTypeRepository.GetByIdAsync(request.Id, cancellationToken);

            return await CreateOrUpdateDeviceType(request, deviceType, cancellationToken);
        }

        private async Task<int> CreateOrUpdateDeviceType(CreateUpdateDeviceTypeCommand command, DeviceType? deviceType, CancellationToken cancellationToken)
        {
            return deviceType is null ? await CreateDeviceType(command, cancellationToken) :
                                        await UpdateDeviceType(command, deviceType, cancellationToken);
        }

        private async Task<int> CreateDeviceType(CreateUpdateDeviceTypeCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating {nameof(DeviceType)}...");

            var deviceType = _mapper.Map<DeviceType>(command);

            await _deviceTypeRepository.AddAsync(deviceType, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(DeviceType) + " is successfully created.");

            return deviceType.Id;
        }

        private async Task<int> UpdateDeviceType(CreateUpdateDeviceTypeCommand command, DeviceType deviceType, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating {nameof(DeviceType)}...");

            _mapper.Map(command, deviceType);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(DeviceType) + " is successfully updated.");

            return deviceType.Id;
        }

        //public async Task<int> Handle(CreateDeviceTypeCommand request, CancellationToken cancellationToken)
        //{
        //    var deviceType = await _deviceTypeRepository.GetByIdAsync(request.Id, cancellationToken);

        //    if (deviceType is null)
        //    {
        //        _logger.LogInformation($"Creating {nameof(DeviceType)}...");

        //        deviceType = _mapper.Map<DeviceType>(request);

        //        await _deviceTypeRepository.AddAsync(deviceType, cancellationToken);

        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        _logger.LogInformation(nameof(DeviceType) + " is successfully created.");

        //        return deviceType.Id;
        //    }
        //    else
        //    {
        //        _logger.LogInformation($"Updating {nameof(DeviceType)}...");

        //        _mapper.Map(request, deviceType);

        //        await _unitOfWork.SaveChangesAsync(cancellationToken);

        //        _logger.LogInformation(nameof(DeviceType) + " is successfully updated.");

        //        return deviceType.Id;
        //    }
        //}
    }
}
