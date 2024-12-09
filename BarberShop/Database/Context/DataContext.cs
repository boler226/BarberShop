using BarberShop.Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BarberShop.Database.Entities;
using BarberShop.Database.EntityTypeConfigurations.Identity;
using BarberShop.Database.EntityTypeConfigurations;

namespace BarberShop.Database.Context
{
    public class DataContext(DbContextOptions<DataContext> options)
    : IdentityDbContext<User, Role, long, IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
            IdentityRoleClaim<long>, IdentityUserToken<long>>(options) {

        public DbSet<Address> Adresses { get; set; }
        public DbSet<Affiliate> Affiliates { get; set; }
        public DbSet<Barbershop> Barbershops { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Service> Services { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            base.OnModelCreating(modelBuilder);

            new UserEntityTypeConfiguration().Configure(modelBuilder.Entity<User>());
            new UserRoleEntityTypeConfiguration().Configure(modelBuilder.Entity<UserRole>());

            new AddressEntityTypeConfiguration().Configure(modelBuilder.Entity<Address>());
            new AffiliateEntityTypeConfiguration().Configure(modelBuilder.Entity<Affiliate>());
            new BarbershopEntityTypeConfiguration().Configure(modelBuilder.Entity<Barbershop>());
            new CityEntityTypeConfiguration().Configure(modelBuilder.Entity<City>());
            new CommentEntityTypeConfiguration().Configure(modelBuilder.Entity<Comment>());
            new CountryEntityTypeConfiguration().Configure(modelBuilder.Entity<Country>());
            new EmployeeEntityTypeConfiguration().Configure(modelBuilder.Entity<Employee>());
            new PositionEntityTypeConfiguration().Configure(modelBuilder.Entity<Position>());
            new ReservationEntityTypeConfiguration().Configure(modelBuilder.Entity<Reservation>());
            new ServiceEntityTypeConfiguration().Configure(modelBuilder.Entity<Service>());

        }
    }
}
