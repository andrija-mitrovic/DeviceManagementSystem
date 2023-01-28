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
    }
}
