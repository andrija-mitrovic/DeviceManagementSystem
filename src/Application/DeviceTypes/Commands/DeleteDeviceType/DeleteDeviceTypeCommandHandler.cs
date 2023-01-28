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
            var deviceType = await _deviceTypeRepository.GetByIdAsync(request.Id, cancellationToken);

            if (deviceType is null)
            {
                _logger.LogError(nameof(DeviceType) + " with id: {DeviceTypeId} was not found.", request.Id);
                throw new DeviceTypeNotFoundException(request.Id);
            }

            _deviceTypeRepository.Delete(deviceType);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(nameof(DeviceType) + " with id: {EmployeeId} is successfully deleted.", request.Id);

            return Unit.Value;
        }
    }
}
