﻿using BarberShop.ViewModels.City;

namespace BarberShop.ViewModels.Country
{
    public class UpdateCountryVm
    {
        public long Id { get; set; }
        public string Name { get; set; } = null!;
        public IFormFile Image { get; set; } = null!;
    }
}
