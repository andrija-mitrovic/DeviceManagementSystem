using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class DevicePropertyValueConfiguration : IEntityTypeConfiguration<DevicePropertyValue>
    {
        public void Configure(EntityTypeBuilder<DevicePropertyValue> builder)
        {
            builder.HasOne(e => e.Device)
                   .WithMany(e => e.DevicePropertyValues)
                   .HasForeignKey(e => e.DeviceId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
