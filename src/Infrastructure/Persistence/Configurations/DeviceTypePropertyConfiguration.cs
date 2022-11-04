using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class DeviceTypePropertyConfiguration : IEntityTypeConfiguration<DeviceTypeProperty>
    {
        public void Configure(EntityTypeBuilder<DeviceTypeProperty> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255);
        }
    }
}
