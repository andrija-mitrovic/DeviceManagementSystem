using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<Device?> GetDeviceByName(string? name)
        {
            return await _dbContext.Devices.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<Device?> GetDeviceDetailById(int id, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Devices.Include(x => x.DeviceType.Parent)
                                          .ThenInclude(x => x.DeviceTypeProperties)
                                          .ThenInclude(x => x.DevicePropertyValue)
                                          .Include(x => x.DeviceType)
                                          .ThenInclude(x => x.DeviceTypeProperties)
                                          .ThenInclude(x => x.DevicePropertyValue)
                                          .AsQueryable();

            if (disableTracking) query = query.AsNoTracking();
            
            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Device?> GetDeviceWithDevicePropertyValueById(int id, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.Devices.Include(x => x.DevicePropertyValues).AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
