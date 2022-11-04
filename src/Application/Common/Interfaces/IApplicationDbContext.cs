using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Device> Devices { get; }
        DbSet<DeviceType> DeviceTypes { get; }
        DbSet<DeviceTypeProperty> DeviceTypeProperties { get; }
        DbSet<DevicePropertyValue> DevicePropertyValues { get; }
    }
}
