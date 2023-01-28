using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class DeviceTypeRepository : GenericRepository<DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository(ApplicationDbContext dbContext) : base(dbContext) { }

        public async Task<DeviceType?> GetDeviceTypeByName(string? name)
        {
            return await _dbContext.DeviceTypes.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<DeviceType?> GetParentDeviceTypeById(int? parentId)
        {
            return await _dbContext.DeviceTypes.Include(x => x.Parent)
                                               .Include(x => x.Parent)
                                               .Include(x => x.DeviceTypeProperties)
                                               .ThenInclude(x => x.DevicePropertyValue)
                                               .FirstOrDefaultAsync(x => x.Id == parentId);
        }

        public async Task<DeviceType?> GetDeviceTypeWitParentById(int id)
        {
            return await _dbContext.DeviceTypes.Include(x => x.Parent)
                                               .Include(x => x.DeviceTypeProperties)
                                               .ThenInclude(x => x.DevicePropertyValue)
                                               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<DeviceType?> GetParentDeviceTypeId(int id)
        {
            return await (from dt in _dbContext.DeviceTypes
                          join pdt in _dbContext.DeviceTypes on dt.ParentId equals pdt.Id
                          where dt.Id == id
                          select pdt).FirstOrDefaultAsync();
        }

        public async Task<DeviceType?> GetDeviceTypeWithPropertiesAndValuesById(int id)
        {
            return await _dbContext.DeviceTypes.Include(x => x.DeviceTypeProperties)
                                               .ThenInclude(x => x.DevicePropertyValue)
                                               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> GetNumberOfDeviceTypeByParentId(int parentId)
        {
            return (await _dbContext.DeviceTypes.Where(x => x.ParentId == parentId).ToListAsync()).Count;
        }

        public async Task<DeviceType?> GetDeviceTypeWithDeviceTypePropertyByIdAsync(int id, bool disableTracking = true, CancellationToken cancellationToken = default)
        {
            var query = _dbContext.DeviceTypes.Include(x => x.Devices)
                                              .Include(x => x.Parent)
                                              .Include(x => x.Children)
                                              .ThenInclude(x => x!.DeviceTypeProperties)
                                              .Include(x => x.DeviceTypeProperties)
                                              .AsQueryable();

            if (disableTracking) query = query.AsNoTracking();

           return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
