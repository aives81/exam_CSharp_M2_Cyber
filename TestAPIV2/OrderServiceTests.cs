using exam_CSharp_M2_Cyber.Services;
using exam_CSharp_M2_Cyber.Models;
using Xunit;

public class OrderServiceTests
{
    [Fact]
    public void CreateOrder_WithValidPromo_ShouldApplyDiscount()
    {
        var db = TestDbContextFactory.Create();

        var productService = new EfProductService(db);
        var promoService = new EfPromoCodeService(db);
        var orderService = new OrderService(productService, promoService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 6 } // 60€
            },
            Promo_Code = "DISCOUNT10"
        };

        var (response, errors) = orderService.CreateOrder(request);

        Assert.Empty(errors);
        Assert.Contains(response.Discounts, d => d.Type == "promo");
    }

    [Fact]
    public void CreateOrder_WithInvalidPromo_ShouldFail()
    {
        var db = TestDbContextFactory.Create();

        var orderService = new OrderService(
            new EfProductService(db),
            new EfPromoCodeService(db));

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 6 }
            },
            Promo_Code = "BADCODE"
        };

        var (_, errors) = orderService.CreateOrder(request);

        Assert.Contains(errors, e => e.Contains("code promo"));
    }
}