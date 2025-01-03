using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.Geoname;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services
{
    public class CountryDataSeeder(
        DataContext context,
        IImageService imageService,
        IConfiguration configuration
        ) : ICountryDataSeeder {
        public async Task SeedAsync() {

            if (!await context.Countries.AnyAsync())
                await CreateCitiesAndCountriesAsync();
        }

        private async Task CreateCitiesAndCountriesAsync() {
            var countryCodes = configuration
                    .GetSection("Seed:CountryCodes")
                    .Get<string[]>();

            if (countryCodes is null)
                throw new Exception("Configuration Seed:CountryCodes is invalid");

            foreach (var countryCode in countryCodes)
                await CreateCountryByCodeAsync(countryCode);
        }

        private async Task CreateCountryByCodeAsync(string countryCode) {
            Faker faker = new Faker();

            var citiesResponve = await GetCitiesFromGeonamesAsync(countryCode);

            using var httpClient = new HttpClient();
            var imageUrl = faker.Image.LoremFlickrUrl(keywords: "city");

            Country country = new Country {
                Name = citiesResponve.First().CountryName,
                Image = await imageService.SaveImageAsync(await imageService.GetImageAsBase64Async(httpClient, imageUrl))
            };

            country.Cities = citiesResponve.Select(c => new City {
                Name = c.Name,
                Latitude = c.Latitude,
                Longitude = c.Longitude
            }).ToArray();

            foreach (var city in country.Cities) {
                imageUrl = faker.Image.LoremFlickrUrl(keywords: "city");
                city.Image = await imageService.SaveImageAsync(await imageService.GetImageAsBase64Async(httpClient, imageUrl));
            }

            await context.Countries.AddAsync(country);
            await context.SaveChangesAsync();
        }

        private async Task<List<Geoname>> GetCitiesFromGeonamesAsync(string countryCode) {
            const string apiUrl = "http://api.geonames.org";
            const string geonamesUsername = "deadlightdie";

            var count = configuration["Seed:CitiesCountPerCountry"]
                    ?? throw new Exception("");

            var geonamesUrl = $"{apiUrl}/searchJSON?country={countryCode}&maxRows={count}&featureClass=P&username={geonamesUsername}";

            using var httpClient = new HttpClient();
            var geonamesResponse = await httpClient.GetFromJsonAsync<GeonameResponse>(geonamesUrl)
                    ?? throw new Exception($"Bad request to {apiUrl}");

            return geonamesResponse.Geonames;
        }
    }
}
