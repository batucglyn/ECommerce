using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Scalar.AspNetCore;
using ECommerce.Infrastructure.Context;
using ECommerce.Domain.Entities.Users;
using ECommerce.Infrastructure;
using ECommerce.Application;
using ECommerce.Application.Products.Commands;
using ECommerce.Application.Mapping;
using ECommerce.Infrastructure.Configurations;
using ECommerce.Infrastructure.Services;
using ECommerce.WebAPI.Middlewares;
using ECommerce.WebAPI.OpenApi; // <-- Transformer burada olmalı

var builder = WebApplication.CreateBuilder(args);

// 1. Veritabanı bağlantısı (EF Core)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// 2. Identity konfigürasyonu (AppUser ile)
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 2;
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// 3. JWT Ayarları
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddScoped<JwtTokenGenerator>();

builder.Services.AddHttpContextAccessor(); // Token içinden UserId almak için

// 4. Custom Dependency Injection Yapıları
builder.Services.AddInfrastructure(); // Repositories
builder.Services.AddApplication();    // Rules, Commands, Queries

// 5. AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

// 6. FluentValidation
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);
builder.Services.AddFluentValidationAutoValidation();

// 7. MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

// 8. CORS
builder.Services.AddCors();

// 9. Authentication (JWT)
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
        };
    });

builder.Services.AddAuthorization();

// 10. Controllers
builder.Services.AddControllers();

// 11. Scalar + Bearer Auth
builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); //  En önemli satır
});

var app = builder.Build();

// Orta katmanlar ve middleware'lar
app.UseCors(policy => policy
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Scalar UI
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // JSON endpoint
}
app.MapScalarApiReference(); // UI endpoint

app.MapControllers();
app.Run();
