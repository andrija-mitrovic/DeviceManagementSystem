using Application.Common.Helpers;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<Device?> GetDeviceByName(string? name);
        Task<Device?> GetDeviceDetailById(int id, bool disableTracking = true, CancellationToken cancellationToken = default);
        Task<Device?> GetDeviceWithDevicePropertyValueById(int id, bool disableTracking = true, CancellationToken cancellationToken = default);
        Task<PagedList<Device>> GetDevicesByParametersAsync(DeviceParameters? deviceParameters, bool disableTracking = true, CancellationToken cancellationToken = default);
    }
}
