using AutoMapper;
using BarberShop.Database.Context;
using BarberShop.Database.Entities;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.Services.Interfaces;
using BarberShop.ViewModels.City;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.Services.ControllerServices
{
    public class CitiesControllerService(
        DataContext context,
        IMapper mapper,
        IImageService imageService
        ) : ICitiesControllerService 
    {
        public async Task CreateAsync(CreateCityVm vm) {
            var city = mapper.Map<City>(vm);
            city.Image = await imageService.SaveImageAsync(vm.Image);

            await context.Cities.AddAsync(city);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception) 
            {
                imageService.DeleteImageIfExists(city.Image);
                throw;
            }
        }
        public async Task UpdateAsync(UpdateCityVm vm) {
            var city = await context.Cities.FirstAsync(c => c.Id == vm.Id);

            string oldImage = city.Image;
            
            city.Name = vm.Name;
            city.Longitude = vm.Longitude;
            city.Latitude = vm.Latitude;
            city.CountryId = vm.CountryId;

            city.Image = await imageService.SaveImageAsync(vm.Image);

            try
            {
                await context.SaveChangesAsync();

                imageService.DeleteImageIfExists(oldImage);
            }
            catch (Exception)
            {
                imageService.DeleteImageIfExists(city.Image);
                throw;
            }
        }

        public async Task DeleteIfExistsAsync(long id)
        {
            var city = await context.Cities.FirstOrDefaultAsync(c => c.Id == id);

            if (city is null)
                return;

            context.Cities.Remove(city);

            await context.SaveChangesAsync();

            imageService.DeleteImageIfExists(city.Image);
        }
    }
}
