using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class BarbershopEntityTypeConfiguration : IEntityTypeConfiguration<Barbershop> {
        public void Configure(EntityTypeBuilder<Barbershop> builder) {
            builder.ToTable("Barbershops");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                    .HasMaxLength(255)
                    .IsRequired();

            builder.Property(b => b.Phone)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasOne(b => b.Adddress)
                    .WithMany()
                    .HasForeignKey(ba => ba.AddressId)
                    .IsRequired();

            builder.HasMany(b => b.Employers)
                   .WithOne()
                   .HasForeignKey(ba => ba.BarbershopId)
                   .IsRequired();

            builder.HasOne(b => b.Affiliate)
                    .WithMany(a => a.Barbershops)
                    .HasForeignKey(ba => ba.AffiliateId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
        }
    }
}
