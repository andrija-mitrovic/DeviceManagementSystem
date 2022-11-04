using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    internal sealed class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(255);

            builder.HasMany(e => e.Children)
                   .WithOne(e => e.Parent)
                   .HasForeignKey(e => e.ParentId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
