﻿using BarberShop.ViewModels.Country;

namespace BarberShop.ViewModels.City
{
    public class CityVm
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

        public string Image { get; set; } = null!;

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public CountryVm Country { get; set; } = null!;
    }
}
