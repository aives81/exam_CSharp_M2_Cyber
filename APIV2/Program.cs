using exam_CSharp_M2_Cyber.Data;
using exam_CSharp_M2_Cyber.Data.Entities;
using exam_CSharp_M2_Cyber.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductService, EfProductService>();
builder.Services.AddScoped<IPromoCodeService, EfPromoCodeService>();

builder.Services.AddOpenApi();

// EF Core In-Memory
builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseInMemoryDatabase("ECommerceDb"));

var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ECommerceDbContext>();

    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new ProductEntity { Id = 1, Name = "Produit A", Price = 10, Stock = 20 },
            new ProductEntity { Id = 2, Name = "Produit B", Price = 15, Stock = 10 },
            new ProductEntity { Id = 3, Name = "Produit C", Price = 8, Stock = 50 },
            new ProductEntity { Id = 4, Name = "Produit D", Price = 25, Stock = 5 },
            new ProductEntity { Id = 5, Name = "Produit E", Price = 5, Stock = 100 }
        );
    }

    if (!db.PromoCodes.Any())
    {
        db.PromoCodes.AddRange(
            new PromoCodeEntity { Code = "DISCOUNT10", DiscountRate = 10 },
            new PromoCodeEntity { Code = "DISCOUNT20", DiscountRate = 20 }
        );
    }

    db.SaveChanges();
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();