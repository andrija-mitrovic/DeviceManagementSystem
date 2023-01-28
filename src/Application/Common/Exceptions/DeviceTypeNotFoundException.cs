using Domain.Entities;

namespace Application.Common.Exceptions
{
    public sealed class DeviceTypeNotFoundException : NotFoundException
    {
        public DeviceTypeNotFoundException(int id) : base($"{nameof(DeviceType)} with id: {id} doesn't exist in the database.") { }
    }
}
