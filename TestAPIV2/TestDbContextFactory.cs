using exam_CSharp_M2_Cyber.Data;
using exam_CSharp_M2_Cyber.Data.Entities;
using Microsoft.EntityFrameworkCore;

public static class TestDbContextFactory
{
    public static ECommerceDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ECommerceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var db = new ECommerceDbContext(options);

        db.Products.AddRange(
            new ProductEntity { Id = 1, Name = "Produit A", Price = 10, Stock = 20 },
            new ProductEntity { Id = 2, Name = "Produit B", Price = 15, Stock = 10 }
        );

        db.PromoCodes.AddRange(
            new PromoCodeEntity { Code = "DISCOUNT10", DiscountRate = 10 },
            new PromoCodeEntity { Code = "DISCOUNT20", DiscountRate = 20 }
        );

        db.SaveChanges();

        return db;
    }
}