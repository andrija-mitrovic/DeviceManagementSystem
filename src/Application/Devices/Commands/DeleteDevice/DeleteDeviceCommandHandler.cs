using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Devices.Commands.DeleteDevice
{
    internal sealed class DeleteDeviceCommandHandler : IRequestHandler<DeleteDeviceCommand, Unit>
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDevicePropertyValueRepository _devicePropertyValueRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteDeviceCommandHandler> _logger;

        public DeleteDeviceCommandHandler(
            IDeviceRepository deviceRepository, 
            IDevicePropertyValueRepository devicePropertyValueRepository,
            IUnitOfWork unitOfWork, 
            ILogger<DeleteDeviceCommandHandler> logger)
        {
            _deviceRepository = deviceRepository;
            _devicePropertyValueRepository = devicePropertyValueRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDeviceCommand request, CancellationToken cancellationToken)
        {
            var device = await _deviceRepository.GetDeviceWithDevicePropertyValueById(request.Id, false, cancellationToken);

            if (device is null)
            {
                _logger.LogError(nameof(Device) + " with id: {DeviceId} was not found.", request.Id);
                throw new DeviceNotFoundException(request.Id);
            }

            _deviceRepository.Delete(device);
            _devicePropertyValueRepository.DeleteRange(device.DevicePropertyValues!);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(Device) + " with id: {DeviceId} is successfully deleted.", request.Id);

            return Unit.Value;
        }
    }
}
