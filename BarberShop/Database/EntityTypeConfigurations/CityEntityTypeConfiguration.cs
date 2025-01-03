using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class CityEntityTypeConfiguration : IEntityTypeConfiguration<City> {
        public void Configure(EntityTypeBuilder<City> builder) {
            builder.ToTable("Cities");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                    .HasMaxLength(255)
                    .IsRequired();

            builder.Property(c => c.Image)
                    .HasMaxLength(255)
                    .IsRequired();

            builder.Property(c => c.Longitude)
              .IsRequired();

            builder.Property(c => c.Latitude)
                   .IsRequired();

            builder.HasOne(c => c.Country)
                    .WithMany(c => c.Cities)
                    .HasForeignKey(c => c.CountryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
