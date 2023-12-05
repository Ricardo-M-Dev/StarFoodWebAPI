using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StarFood.Application.CommandHandlers;
using StarFood.Application.Handlers;
using StarFood.Application.Interfaces;
using StarFood.Domain.Commands;
using StarFood.Domain.Entities;
using MediatR;
using StarFood.Domain.Repositories;
using StarFood.Infrastructure.Auth;
using StarFood.Infrastructure.Data;
using StarFood.Infrastructure.Data.Repositories;
using StarFood.Infrastructure.Middleware;
using StarsFoodAPI.Services.HttpContext;
using System.Text;
using StarFood.Application.Communication;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
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

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost8000",
        builder => builder.WithOrigins("http://localhost:8000")
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials()
                          .SetIsOriginAllowed(origin => true));
});

builder.Services.AddDbContext<StarFoodDbContext>(options =>
{
    string connectionString = configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString,
                    ServerVersion.AutoDetect(connectionString),
                    builder => builder.MigrationsAssembly("StarFood.Infrastructure"));
}, ServiceLifetime.Scoped);


builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<Auth>();
builder.Services.AddScoped<RequestState>();
builder.Services.AddScoped<IMediatorHandler, MediatorHandler>();
builder.Services.AddScoped<IOrdersRepository, OrderRepository>();
builder.Services.AddScoped<IProductsRepository, ProductRepository>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();
builder.Services.AddScoped<IVariationsRepository, VariationsRepository>();
builder.Services.AddScoped<IRestaurantsRepository, RestaurantsRepository>();
builder.Services.AddScoped<IOrderProductsRepository, OrderProductsRepository>();
builder.Services.AddMediatR(typeof(MediatorHandler).GetTypeInfo().Assembly);

builder.Services.AddScoped<ICommandHandler<CreateOrderCommand, Orders>, CreateOrderCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateVariationCommand, Variations>, CreateVariationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateVariationCommand, Variations>, UpdateVariationCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateRestaurantCommand, Restaurants>, CreateRestaurantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<UpdateRestaurantCommand, Restaurants>, UpdateRestaurantCommandHandler>();
builder.Services.AddScoped<ICommandHandler<CreateOrderProductsCommand, OrderProducts>, CreateOrderProductsCommandHandler>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowLocalhost8000");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<HttpMiddleware>();
app.MapControllers();
app.Run();
