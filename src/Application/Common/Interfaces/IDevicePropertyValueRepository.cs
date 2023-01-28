using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IDevicePropertyValueRepository : IGenericRepository<DevicePropertyValue>
    {
        Task<List<DevicePropertyValue?>> GetDevicePropertyValuesByDeviceId(int deviceId);
        Task<List<DevicePropertyValue?>> GetParentDevicePropertyValuesByDeviceId(int deviceId);
    }
}
