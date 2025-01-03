using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.Interfaces;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services {
    public class DataSeeder(
        DataContext context,
        IImageService imageService
        ) : IDataSeeder
    {
        public async Task SeedAsync() {
            using var transaction = await context.Database.BeginTransactionAsync();

            try {
                if (!await context.Addresses.AnyAsync())
                    await CreateAddressesAsync();

                if (!await context.Affiliates.AnyAsync())
                    await CreateAffiliatesAsync();

                if (!await context.Barbershops.AnyAsync())
                    await CreateBarbershopsAsync();

                if (!await context.Positions.AnyAsync())
                    CreatePositions();

                if(!await context.Employees.AnyAsync())
                    await CreateEmployeesAsync();

                if (!await context.Comments.AnyAsync())
                    await CreateCommentsAsync();

                if (!await context.Services.AnyAsync())
                    CreateServices();

                await transaction.CommitAsync();
            }
            catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private async Task CreateAddressesAsync() {
            Faker faker = new Faker();
            Random random = new Random();

            var citiesId = await context.Cities.Select(c => c.Id).ToListAsync();
            var addresses = new List<Address>();

            for (int i = 0; i < 100; i++) {
                var cityId = faker.PickRandom(citiesId);
                var city = context.Cities.Where(c => c.Id == cityId).FirstOrDefault();

                if (city != null) {
                    var address = new Address {
                        Street = faker.Address.StreetName(),
                        HouseNumber = faker.Address.BuildingNumber(),
                        CityId = cityId,
                        Latitude = city.Latitude + (random.NextDouble() * 0.01 - 0.005),
                        Longitude = city.Longitude + (random.NextDouble() * 0.01 - 0.005)
                    };

                    addresses.Add(address);
                }
            }

            context.Addresses.AddRange(addresses);
            context.SaveChanges();
        }

        private async Task CreateAffiliatesAsync() {
            Faker faker = new Faker();

            var citiesId = await context.Cities.Select(c => c.Id).ToListAsync();
            var affiliates = new List<Affiliate>();

            for (int i = 0; i < 10; i++) {
                var cityId = faker.PickRandom(citiesId);
                var city = context.Cities.Where(c => c.Id == cityId).FirstOrDefault();

                if (city != null) {
                    var affiliate = new Affiliate {
                        Phone = faker.Phone.PhoneNumber(),
                        CityId = cityId
                    };

                    affiliates.Add(affiliate);
                }
            }

            context.Affiliates.AddRange(affiliates);
            context.SaveChanges();
        }

        private async Task CreateBarbershopsAsync() {
            Faker faker = new Faker();

            var addressesId = await context.Addresses.Select(a => a.Id).ToListAsync();
            var affiliatesId = await context.Affiliates.Select(a => a.Id).ToListAsync();
            var barbershops = new List<Barbershop>();   
            
            for ( int i = 0; i < 20; i++) {
                var address = context.Addresses
                        .Where(a => a.Id == faker
                        .PickRandom(addressesId))
                        .FirstOrDefault();

                var affiliate = context.Affiliates
                        .Where(a => a.Id == faker
                        .PickRandom(affiliatesId))
                        .FirstOrDefault();

                if (affiliate != null && address != null) {
                    var barbershop = new Barbershop {
                        Name = faker.Company.CompanyName(),
                        Phone = faker.Phone.PhoneNumberFormat(0),
                        AddressId = address.Id,
                        AffiliateId = affiliate.Id
                    };

                    barbershops.Add(barbershop);
                }
            }
            context.Barbershops.AddRange(barbershops);
            context.SaveChanges();
        }

        private void CreatePositions() {
            Faker faker = new Faker();

            var positions = new List<Position>();

            for (int i = 0; i < 10; i++) {
                var position = new Position {
                    Name = faker.Commerce.Department()
                };

                positions.Add(position);
            }

            context.Positions.AddRange(positions);
            context.SaveChanges();
        }

        private async Task CreateEmployeesAsync() {
            Faker faker = new Faker();
            Random random = new Random();

            var positionsId = await context.Positions.Select(p => p.Id).ToArrayAsync();
            var barbershopsId = await context.Barbershops.Select(b => b.Id).ToArrayAsync();
            var employers = new List<Employee>();

            for (int i = 0; i < 50; i++) {
                var position = context.Positions
                        .Where(p => p.Id == faker
                        .PickRandom(positionsId))
                        .FirstOrDefault();

                var barbershop = context.Barbershops
                        .Where(b => b.Id == faker
                        .PickRandom(barbershopsId))
                        .FirstOrDefault();

                using var httpClient = new HttpClient();
                var imageUrl = faker.Image.LoremFlickrUrl(keywords: "human");

                if (position != null && barbershop != null) {
                    var employer = new Employee {
                        Name = faker.Name.FullName(),
                        Image = await imageService.SaveImageAsync(await imageService.GetImageAsBase64Async(httpClient, imageUrl)),
                        Rating = 0,
                        PositionId = position.Id,
                        BarbershopId = barbershop.Id
                    };

                    employers.Add(employer);
                }
            }
            
            context.AddRange(employers);
            context.SaveChanges();
        }

        private async Task CreateCommentsAsync() {
            Faker faker = new Faker();
            Random random = new Random();

            var employeesId = await context.Employees.Select(e => e.Id).ToArrayAsync();
            var usersId = await context.Users.Select(u => u.Id).ToArrayAsync();
            var comments = new List<Comment>();

            for (int i = 0; i < 150; i++) {
                var employee = context.Employees
                        .Where(e => e.Id == faker
                        .PickRandom(employeesId))
                        .FirstOrDefault();

                var users = context.Users
                        .Where(u => u.Id == faker
                        .PickRandom(usersId))
                        .FirstOrDefault();

                if (employee != null && users != null) {
                    var comment = new Comment {
                        Rating = random.Next(0, 6),
                        Description = faker.Commerce.ProductDescription(),
                        EmployeeId = employee.Id,
                        UserId = users.Id
                    };
                    comments.Add(comment);
                }
            }
            
            context.AddRange(comments);
            context.SaveChanges();
        }

        private void CreateServices() {
            Faker faker = new Faker();
            Random random = new Random();

            var services = new List<Service>(); 

            for (int i = 0; i < 25;  i++) {
                var service = new Service {
                    Name = faker.Commerce.Product(),
                    Price = random.NextDouble() * (800 - 100) + 100,
                    Time = random.NextDouble() * (90 - 15) + 100
                };
                services.Add(service);
            }
            context.Services.AddRange(services);
            context.SaveChanges();
        }
    }
}
