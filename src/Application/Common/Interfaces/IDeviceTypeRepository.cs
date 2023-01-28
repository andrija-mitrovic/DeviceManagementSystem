using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IDeviceTypeRepository : IGenericRepository<DeviceType>
    {
        Task<DeviceType?> GetDeviceTypeByName(string? name);
        Task<DeviceType?> GetParentDeviceTypeById(int? parentId);
        Task<DeviceType?> GetDeviceTypeWitParentById(int id);
        Task<DeviceType?> GetParentDeviceTypeId(int id);
        Task<DeviceType?> GetDeviceTypeWithPropertiesAndValuesById(int id);
        Task<int> GetNumberOfDeviceTypeByParentId(int parentId);
        Task<DeviceType?> GetDeviceTypeWithDeviceTypePropertyByIdAsync(int id, bool disableTracking = true, CancellationToken cancellationToken = default);
    }
}
