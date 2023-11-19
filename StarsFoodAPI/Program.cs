using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StarFood.Application.CommandHandlers;
using StarFood.Application.Handlers;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Auth;
using StarFood.Infrastructure.Data;
using StarFood.Infrastructure.Data.Repositories;
using StarFood.Infrastructure.Middleware;
using StarsFoodAPI.Services.HttpContext;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .Build();

var jwtSection = configuration.GetSection("Jwt");
var jwtIssuer = jwtSection["Issuer"];
var jwtAudience = jwtSection["Audience"];
var jwtKey = jwtSection["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddDbContext<StarFoodDbContext>(options =>
{
    string connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly("StarFood.Infrastructure"));
}, ServiceLifetime.Scoped);

builder.Services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IVariationsRepository, VariationsRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<ICommandHandler<CreateProductCommand, Products>, CreateProductCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateProductCommand, Products>, UpdateProductCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateCategoryCommand, Categories>, CreateCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCategoryCommand, Categories>, UpdateCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateCategoryCommand, Categories>, UpdateCategoryCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateVariationCommand, Variations>, CreateVariationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateVariationCommand, Variations>, UpdateVariationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateRestaurantCommand, Restaurants>, CreateRestaurantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateRestaurantCommand, Restaurants>, UpdateRestaurantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateUserCommand, Users>, CreateUserCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateUserCommand, Users>, UpdateUserCommandHandler>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<AuthenticatedContext>();
builder.Services.AddScoped<Auth>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<StarFoodDbContext>();
    dataContext.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<HttpMiddleware>();
app.MapControllers();
app.Run();
