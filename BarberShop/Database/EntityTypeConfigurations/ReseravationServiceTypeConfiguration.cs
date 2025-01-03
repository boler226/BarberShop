using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    public class ReseravationServiceTypeConfiguration : IEntityTypeConfiguration<ReservationService> {
        public void Configure(EntityTypeBuilder<ReservationService> builder) {
            builder.ToTable("ReservationServices");
            
            builder.HasKey(rs => new {rs.ReservationId, rs.ServiceId});

            builder.HasOne(rs => rs.Reservation)
                    .WithMany(r => r.ReservationService)
                    .HasForeignKey(r => r.ReservationId);

            builder.HasOne(rs => rs.Service)
                    .WithMany(s => s.ReservationService)
                    .HasForeignKey(s => s.ServiceId);
        }
    }
}
