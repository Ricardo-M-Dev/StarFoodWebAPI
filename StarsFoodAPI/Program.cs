
using Microsoft.EntityFrameworkCore;
using StarFood.Application.CommandHandlers;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Data;
using StarFood.Infrastructure.Data.Repositories;
using StarsFoodAPI.Services.HttpContext;

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
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IVariationsRepository, VariationsRepository>();

builder.Services.AddScoped<ICommandHandler<CreateProductCommand, Products>, CreateProductCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateProductCommand, Products>, UpdateProductCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateCategoryCommand, Categories>, CreateCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCategoryCommand, Categories>, UpdateCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCategoryCommand, Categories>, UpdateCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateVariationCommand, Variations>, CreateVariationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateVariationCommand, Variations>, UpdateVariationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateRestaurantCommand, Restaurants>, CreateRestaurantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateRestaurantCommand, Restaurants>, UpdateRestaurantCommandHandler>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<AuthenticatedContext>();

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
