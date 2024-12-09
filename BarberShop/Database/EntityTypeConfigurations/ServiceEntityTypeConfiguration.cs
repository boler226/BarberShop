using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class ServiceEntityTypeConfiguration : IEntityTypeConfiguration<Service> {
        public void Configure(EntityTypeBuilder<Service> builder) {
            builder.ToTable("Services");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .HasMaxLength(255)
                   .IsRequired();

            builder.HasOne(s => s.Reservation)
                   .WithMany(r => r.Services)
                   .HasForeignKey(s => s.ReservationId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
