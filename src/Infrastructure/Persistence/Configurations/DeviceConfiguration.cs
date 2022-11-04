using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255);

            builder.HasOne(c => c.DeviceType)
                   .WithMany(c => c.Devices)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
