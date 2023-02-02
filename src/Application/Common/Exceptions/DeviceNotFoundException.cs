using Domain.Entities;

namespace Application.Common.Exceptions
{
    public sealed class DeviceNotFoundException : NotFoundException
    {
        public DeviceNotFoundException(int id) : base($"{nameof(Device)} with id: {id} doesn't exist in the database.") { }
    }
}
