using Domain.Entities;

namespace Application.Common.Exceptions
{
    public sealed class DeviceTypeWithChildEntityBadRequestException : BadRequestException
    {
        public DeviceTypeWithChildEntityBadRequestException(int id)
            : base($"{nameof(DeviceType)} with id: {id} has child entity so it cannot be deleted.")
        {
        }
    }
}
