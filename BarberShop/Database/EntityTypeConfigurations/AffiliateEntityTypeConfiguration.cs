using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class AffiliateEntityTypeConfiguration : IEntityTypeConfiguration<Affiliate> {
        public void Configure(EntityTypeBuilder<Affiliate> builder) {
            builder.ToTable("Affiliates");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Phone)
                    .HasMaxLength(255)
                    .IsRequired();

            builder.HasOne(a => a.City)
                    .WithMany(c => c.Affiliates)
                    .HasForeignKey(ac => ac.CityId)
                    .IsRequired();

            builder.HasMany(a => a.Barbershops)
                    .WithOne(ab => ab.Affiliate)
                    .HasForeignKey(ab => ab.AffiliateId)
                    .IsRequired();
        }
    }
}
