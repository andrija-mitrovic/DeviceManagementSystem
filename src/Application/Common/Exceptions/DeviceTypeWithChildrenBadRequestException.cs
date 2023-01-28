using Domain.Entities;

namespace Application.Common.Exceptions
{
    public sealed class DeviceTypeWithChildrenBadRequestException : BadRequestException
    {
        public DeviceTypeWithChildrenBadRequestException(int id)
            : base($"{nameof(DeviceType)} with id: {id} has children so it cannot be deleted.")
        {
        }
    }
}
