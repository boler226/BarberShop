using AutoMapper;

using BarberShop.ViewModels.Country;
using BarberShop.ViewModels.City;
using BarberShop.Database.Entities;
using BarberShop.ViewModels.Address;
using BarberShop.ViewModels.Affiliate;
using BarberShop.ViewModels.Barbershop;
using BarberShop.ViewModels.Position;
using BarberShop.ViewModels.Reservation;
using BarberShop.ViewModels.Service;
using BarberShop.ViewModels.Employee;
using BarberShop.ViewModels.Comment;


namespace BarberShop.Mapper
{
    public class AppMapProfile : Profile {
        public AppMapProfile() {
            CreateMap<Country, CountryVm>();
            CreateMap<CreateCountryVm, Country>()
                    .ForMember(c => c.Image, opt => opt.Ignore());

            CreateMap<City, CityVm>();
            CreateMap<CreateCityVm, City>()
                    .ForMember(c => c.Image, opt => opt.Ignore());

            CreateMap<Address, AddressVm>();
            CreateMap<CreateAddressVm, Address>();

            CreateMap<Affiliate, AffiliateVm>();
            CreateMap<CreateAffiliateVm, Affiliate>();

            CreateMap<Barbershop, BarbershopVm>();
            CreateMap<CreateBarbershopVm, Barbershop>();

            CreateMap<Position, PositionVm>();
            CreateMap<CreatePositionVm, Position>();

            CreateMap<Reservation, ReservationVm>();
            CreateMap<CreateReservationVm, Reservation>();

            CreateMap<Service, ServiceVm>();
            CreateMap<CreateServiceVm, Service>();

            CreateMap<Employee, EmployeeVm>();
            CreateMap<CreateEmployeeVm, Employee>();

            CreateMap<Comment, CommentVm>();
            CreateMap<CreateCommentVm, Comment>();
        }
    }
}
