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

            builder.HasMany(s => s.ReservationService)
                   .WithOne(rs => rs.Service)
                   .HasForeignKey(rs => rs.ServiceId);
        }
    }
}
