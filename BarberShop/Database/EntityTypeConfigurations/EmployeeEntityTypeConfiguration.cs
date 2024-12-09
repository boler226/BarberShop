using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder) {
            builder.ToTable("Employees");

            builder.HasKey(e => e.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Image)
                    .HasMaxLength(255)
                    .IsRequired();

            builder.HasOne(e => e.Position)
                   .WithOne(p => p.Employee)
                   .HasForeignKey<Employee>(e => e.PositionId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();

            builder.HasMany(e => e.Comments)
                   .WithOne(c => c.Employee)
                   .HasForeignKey(e => e.EmployeeId)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired();
        }
    }
}
