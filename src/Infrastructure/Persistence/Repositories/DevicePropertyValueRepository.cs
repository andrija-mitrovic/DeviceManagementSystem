using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class DevicePropertyValueRepository : GenericRepository<DevicePropertyValue>, IDevicePropertyValueRepository
    {
        public DevicePropertyValueRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<List<DevicePropertyValue?>> GetDevicePropertyValuesByDeviceId(int deviceId)
        {
            return await _dbContext.Devices.Include(x => x.DeviceType)
                                           .ThenInclude(x => x.DeviceTypeProperties)
                                           .ThenInclude(x => x.DevicePropertyValue)
                                           .Where(x => x.Id == deviceId)
                                           .SelectMany(x => x.DeviceType.DeviceTypeProperties.Select(y => y.DevicePropertyValue))
                                           .ToListAsync();
        }

        public async Task<List<DevicePropertyValue?>> GetParentDevicePropertyValuesByDeviceId(int deviceId)
        {
            return await _dbContext.Devices.Include(x => x.DeviceType)
                                           .ThenInclude(x => x.Parent)
                                           .ThenInclude(x => x.DeviceTypeProperties)
                                           .ThenInclude(x => x.DevicePropertyValue)
                                           .Where(x => x.Id == deviceId)
                                           .SelectMany(x => x.DeviceType.Parent.DeviceTypeProperties.Select(y => y.DevicePropertyValue))
                                           .ToListAsync();
        }
    }
}
