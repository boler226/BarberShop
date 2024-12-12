using BarberShop.Database.Context;
using BarberShop.Mapper;
using BarberShop.Services;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.Services.ControllerServices;
using BarberShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var assemblyName = AssemblyService.GetAssemblyName();


builder.Services.AddDbContext<DataContext>(
    options => {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("PostgreSQLConnection"),
            npgsqlOptions => npgsqlOptions.MigrationsAssembly(assemblyName)
        );

        if (builder.Environment.IsDevelopment()) {  
            options.EnableSensitiveDataLogging(); // Логування чутливих даних
        }
    }
);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AppMapProfile));

builder.Services.AddTransient<IImageService, ImageService>();

builder.Services.AddTransient<IAddressesControllerService, AddressesControllerService>();
builder.Services.AddTransient<IAffiliateControllerService, AffiliateControllerService>();
builder.Services.AddTransient<ICountriesControllerService, CountriesControllerService>();
builder.Services.AddTransient<ICitiesControllerService, CitiesControllerService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

string imagesDirPath = app.Services.GetRequiredService<IImageService>().ImagesDir;

if (!Directory.Exists(imagesDirPath))
{
    Directory.CreateDirectory(imagesDirPath);
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imagesDirPath),
    RequestPath = "/images"
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
