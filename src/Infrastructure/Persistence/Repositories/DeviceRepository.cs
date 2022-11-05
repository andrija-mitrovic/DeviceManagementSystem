using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories
{
    internal sealed class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(ApplicationDbContext dbContext) : base(dbContext) { }
    }
}
