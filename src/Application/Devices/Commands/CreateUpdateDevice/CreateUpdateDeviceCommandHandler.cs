using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Devices.Commands.CreateUpdateDevice
{
    internal sealed class CreateUpdateDeviceCommandHandler : IRequestHandler<CreateUpdateDeviceCommand, int>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateUpdateDeviceCommandHandler> _logger;

        public CreateUpdateDeviceCommandHandler(
            IDeviceRepository deviceRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CreateUpdateDeviceCommandHandler> logger)
        {
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> Handle(CreateUpdateDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetByIdAsync(request.Id, cancellationToken);

            return await CreateOrUpdateDevice(request, device, cancellationToken);
        }

        private async Task<int> CreateOrUpdateDevice(CreateUpdateDeviceCommand command, Device? device, CancellationToken cancellationToken)
        {
            return device is null ? await CreateDevice(command, cancellationToken) : await UpdateDevice(command, device, cancellationToken);
        }

        private async Task<int> CreateDevice(CreateUpdateDeviceCommand command, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Creating {nameof(Device)}...");

            var device = _mapper.Map<Device>(command);

            await _deviceRepository.AddAsync(device, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(Device) + " is successfully created.");

            return device.Id;
        }

        private async Task<int> UpdateDevice(CreateUpdateDeviceCommand command, Device device, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Updating {nameof(Device)}...");

            _mapper.Map(command, device);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(Device) + " with Id: {DeviceId} is successfully updated.", device.Id);

            return device.Id;
        }
    }
}
