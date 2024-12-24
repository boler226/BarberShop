using BarberShop.Database.Context;
using BarberShop.Mapper;
using BarberShop.Services;
using BarberShop.Services.ControllerServices.Interfaces;
using BarberShop.Services.ControllerServices;
using BarberShop.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using BarberShop.Database.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using FluentValidation;
using BarberShop.Validators.Adresses;
using BarberShop.Validators.Affiliate;

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

builder.Services
        .AddIdentity<User, Role>(options => {
            options.Stores.MaxLengthForKeys = 128;

            options.Password.RequiredLength = 8;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddEntityFrameworkStores<DataContext>()
        .AddDefaultTokenProviders();

var singinKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(
                builder.Configuration["Authentication:Jwt:SecretKEy"]
                    ?? throw new NullReferenceException("Authentication:Jwt:SecretKey")
        )
);

builder.Services
        .AddAuthentication(options => {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options => {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters() {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = singinKey,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        });


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(AppMapProfile));

builder.Services.AddTransient<IImageService, ImageService>();
builder.Services.AddTransient<IExistingEntityCheckerService, ExistingEntityCheckerService>();
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateAddressValidator>();

builder.Services.AddTransient<IAddressesControllerService, AddressesControllerService>();
builder.Services.AddTransient<IAffiliateControllerService, AffiliateControllerService>();
builder.Services.AddTransient<ICountriesControllerService, CountriesControllerService>();
builder.Services.AddTransient<ICitiesControllerService, CitiesControllerService>();
builder.Services.AddTransient<IBarbershopControllerService, BarbershopControllerService>();
builder.Services.AddTransient<IPositionControllerService, PositionControllerService>();
builder.Services.AddTransient<IReservationControllerService, ReservationsControllerService>();
builder.Services.AddTransient<IServicesControllerService, ServicesControllerService>();
builder.Services.AddTransient<IEmployeesControllerService, EmployeesControllerService>();
builder.Services.AddTransient<ICommentsControllerService, CommentsControllerService>(); 



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
