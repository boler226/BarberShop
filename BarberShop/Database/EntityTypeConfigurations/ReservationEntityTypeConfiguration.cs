using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class ReservationEntityTypeConfiguration : IEntityTypeConfiguration<Reservation> {
        public void Configure(EntityTypeBuilder<Reservation> builder) {
            builder.ToTable("Reservations");

            builder.HasKey(r => r.Id);

            builder.HasMany(r => r.Services)
                   .WithOne(s => s.Reservation)
                   .HasForeignKey(r => r.ReservationId)
                   .IsRequired();

            builder.HasOne(r => r.Employee)
                   .WithMany(e => e.Reservations)
                   .HasForeignKey(r => r.EmployeeId)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired();

            builder.HasOne(r => r.User)
                   .WithMany(u => u.Reservations)
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
