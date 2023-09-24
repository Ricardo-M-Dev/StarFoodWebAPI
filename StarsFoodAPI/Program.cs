
using Microsoft.EntityFrameworkCore;
using StarFood.Application.Interfaces;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarFood.Infrastructure.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

builder.Services.AddDbContext<StarFoodDbContext>(options =>
{
    string connectionString = configuration.GetConnectionString("DefaultConnection");

    options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly("StarFood.Infrastructure"));
});

builder.Services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
builder.Services.AddScoped<IDishesRepository, DishesRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IProductTypesRepository, ProductTypesRepository>();
builder.Services.AddScoped<IDishesProductVariationsRepository, DishesProductVariationsRepository>();
builder.Services.AddScoped<IProductVariationsRepository, ProductVariationsRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
