using AutoMapper;

using BarberShop.ViewModels.Country;
using BarberShop.ViewModels.City;
using BarberShop.Database.Entities;


namespace BarberShop.Mapper
{
    public class AppMapProfile : Profile {
        public AppMapProfile() {
            CreateMap<Country, CountryVm>();
            CreateMap<CreateCountryVm, Country>()
                    .ForMember(c => c.Image, opt => opt.Ignore());
        }
    }
}
