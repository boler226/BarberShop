using BarberShop.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberShop.Database.EntityTypeConfigurations
{
    internal class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment> {
        public void Configure(EntityTypeBuilder<Comment> builder) {
            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Description)
                    .HasMaxLength(1000)
                    .IsRequired();

            builder.HasOne(c => c.Employee)
                    .WithMany(e => e.Comments)
                    .HasForeignKey(e => e.EmployeeId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

            builder.HasOne(c => c.User)
                    .WithMany(u => u.Comments)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
        }
    }
}
