using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<Device?> GetDeviceByName(string? name);
    }
}
