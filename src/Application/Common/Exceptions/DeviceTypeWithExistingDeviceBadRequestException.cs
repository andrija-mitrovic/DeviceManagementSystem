using Domain.Entities;

namespace Application.Common.Exceptions
{
    public sealed class DeviceTypeWithExistingDeviceBadRequestException : BadRequestException
    {
        public DeviceTypeWithExistingDeviceBadRequestException():
            base($"There is at least one {nameof(Device)} with this {nameof(DeviceType)} in the database.")
        { }
    }
}
