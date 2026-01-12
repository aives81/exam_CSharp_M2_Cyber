using exam_CSharp_M2_Cyber.Models;
using exam_CSharp_M2_Cyber.Services;

namespace APITest;

public class OrderServiceTests
{
    [Fact]
    public void CreateOrder_ValidOrder_ShouldSucceed()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 2 }
            }
        };

        var (response, errors) = orderService.CreateOrder(request);

        Assert.NotNull(response);
        Assert.Empty(errors);
        Assert.Equal(20, response.Total);
    }
    
    [Fact]
    public void CreateOrder_ProductDoesNotExist_ShouldReturnError()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 999, Quantity = 1 }
            }
        };

        var (_, errors) = orderService.CreateOrder(request);

        Assert.Single(errors);
        Assert.Contains("n'existe pas", errors[0]);
    }

    [Fact]
    public void CreateOrder_QuantityGreaterThanStock_ShouldReturnError()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 2, Quantity = 100 }
            }
        };

        var (_, errors) = orderService.CreateOrder(request);

        Assert.Single(errors);
        Assert.Contains("Il ne reste que", errors[0]);
    }

    [Fact]
    public void CreateOrder_QuantityGreaterThanFive_ShouldApplyProductDiscount()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 6 }
            }
        };

        var (response, _) = orderService.CreateOrder(request);

        // 10€ * 6 = 60€ → -10% = 54€
        Assert.Equal(54, response.Total);
    }

    [Fact]
    public void CreateOrder_TotalGreaterThan100_ShouldApplyOrderDiscount()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 12 }
            }
        };

        var (response, _) = orderService.CreateOrder(request);

        Assert.Contains(response.Discounts, d => d.Type == "order");
    }


    [Fact]
    public void CreateOrder_InvalidPromoCode_ShouldReturnError()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 6 }
            },
            Promo_Code = "FAKECODE"
        };

        var (_, errors) = orderService.CreateOrder(request);

        Assert.Single(errors);
        Assert.Contains("code promo est invalide", errors[0]);
    }

    [Fact]
    public void CreateOrder_PromoCodeBelow50_ShouldReturnError()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 2 } // 20€
            },
            Promo_Code = "DISCOUNT10"
        };

        var (_, errors) = orderService.CreateOrder(request);

        Assert.Single(errors);
        Assert.Contains("50€", errors[0]);
    }

    [Fact]
    public void CreateOrder_ValidPromo20_ShouldApplyDiscount()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 6 } // 54€
            },
            Promo_Code = "DISCOUNT20"
        };

        var (response, _) = orderService.CreateOrder(request);

        Assert.Contains(response.Discounts, d => d.Type == "promo");
    }
    
    [Fact]
    public void CreateOrder_PromoAndOrderDiscount_ShouldCumulate()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 12 } // >100€
            },
            Promo_Code = "DISCOUNT10"
        };

        var (response, _) = orderService.CreateOrder(request);

        Assert.Equal(2, response.Discounts.Count);
    }

    [Fact]
    public void CreateOrder_ShouldUpdateStock()
    {
        var productService = new ProductService();
        var orderService = new OrderService(productService);

        var request = new OrderRequest
        {
            Products = new()
            {
                new OrderProductRequest { Id = 1, Quantity = 2 }
            }
        };

        orderService.CreateOrder(request);

        var product = productService.GetAllProducts().First(p => p.Id == 1);
        Assert.Equal(18, product.Stock);
    }
}