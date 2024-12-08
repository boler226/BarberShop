using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class AddressEntityTypeConfiguration : IEntityTypeConfiguration<Address> {
        public void Configure(EntityTypeBuilder<Address> builder) {
            builder.ToTable("Addresses");

            builder.Property(a => a.Street)
                    .HasMaxLength(255)
                    .IsRequired();

            builder.Property(a => a.HouseNumber)
                    .HasMaxLength(50)
                    .IsRequired();

            builder.Property(a => a.Longitude)
                    .IsRequired();

            builder.Property(a => a.Latitude)
                   .IsRequired();

            builder.HasOne(a => a.City)
                    .WithMany(c => c.Addresses)
                    .HasForeignKey(ac => ac.CityId)
                    .IsRequired();
        }
    }
}
