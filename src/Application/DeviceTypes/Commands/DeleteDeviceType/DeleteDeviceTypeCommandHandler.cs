using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.DeviceTypes.Commands.DeleteDeviceType
{
    internal sealed class DeleteDeviceTypeCommandHandler : IRequestHandler<DeleteDeviceTypeCommand, Unit>
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteDeviceTypeCommandHandler> _logger;

        public DeleteDeviceTypeCommandHandler(
            IDeviceTypeRepository deviceTypeRepository, 
            IUnitOfWork unitOfWork, 
            ILogger<DeleteDeviceTypeCommandHandler> logger)
        {
            _deviceTypeRepository = deviceTypeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteDeviceTypeCommand request, CancellationToken cancellationToken)
        {
            var deviceType = await _deviceTypeRepository.GetDeviceTypeWithDeviceTypePropertyByIdAsync(request.Id, true, cancellationToken);

            if (deviceType is null)
            {
                _logger.LogError(nameof(DeviceType) + " with id: {DeviceTypeId} was not found.", request.Id);
                throw new DeviceTypeNotFoundException(request.Id);
            }

            if (ContainsAnyChildDeviceType(deviceType))
            {
                _logger.LogError(nameof(DeviceType) + " with id: {DeviceTypeId} has child entity.", request.Id);
                throw new DeviceTypeWithChildEntityBadRequestException(request.Id);
            }

            if (ContainsAnyDevice(deviceType))
            {
                _logger.LogError($"There is at least one {nameof(Device)} with this {nameof(DeviceType)} in the database.");
                throw new DeviceTypeWithExistingDeviceBadRequestException();
            }

            _deviceTypeRepository.Delete(deviceType);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(DeviceType) + " with id: {EmployeeId} is successfully deleted.", request.Id);

            return Unit.Value;
        }

        private static bool ContainsAnyChildDeviceType(DeviceType deviceType)
        {
            return deviceType.Devices != null && deviceType.Children!.Any();
        }

        private static bool ContainsAnyDevice(DeviceType deviceType)
        {
            return deviceType.Devices != null && deviceType.Devices.Any();
        }
    }
}
