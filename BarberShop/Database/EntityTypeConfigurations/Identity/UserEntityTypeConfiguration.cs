using BarberShop.Database.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BarberShop.Database.EntityTypeConfigurations.Identity
{
    internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(u => u.Image)
                    .IsRequired()
                    .HasMaxLength(200);
        }
    }
}
