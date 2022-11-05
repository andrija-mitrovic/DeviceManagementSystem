using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class DevicePropertyValueRepository : GenericRepository<DevicePropertyValue>, IDevicePropertyValueRepository
    {
        public DevicePropertyValueRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
