using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class PositionEntityTypeConfiguration : IEntityTypeConfiguration<Position> {
        public void Configure(EntityTypeBuilder<Position> builder) {
            builder.ToTable("Positions");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                 .HasMaxLength(255)
                 .IsRequired();
        }
    }
}
