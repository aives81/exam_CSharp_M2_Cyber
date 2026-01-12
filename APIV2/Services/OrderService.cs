using exam_CSharp_M2_Cyber.Models;
using exam_CSharp_M2_Cyber.Services.Interfaces;

namespace exam_CSharp_M2_Cyber.Services;

public class OrderService : IOrderService
{
    private readonly IProductService _productService;

    public OrderService(IProductService productService)
    {
        _productService = productService;
    }
    
    public (OrderResponse? response, List<string> errors) CreateOrder(OrderRequest request)
    {
        var errors = new List<string>();
        var response = new OrderResponse();

        var products = _productService.GetProducts();
        decimal totalBeforeDiscount = 0;

        foreach (var item in request.Products)
        {
            var product = products.FirstOrDefault(p => p.Id == item.Id);

            // Produit inexistant
            if (product == null)
            {
                errors.Add($"Le produit avec l'identifiant {item.Id} n'existe pas");
                continue;
            }

            // Stock insuffisant
            if (item.Quantity > product.Stock)
            {
                errors.Add($"Il ne reste que {product.Stock} exemplaire pour le produit {product.Name}");
                continue;
            }

            decimal unitPrice = product.Price;

            // Remise quantité > 5
            if (item.Quantity > 5)
            {
                unitPrice *= 0.9m;
            }

            decimal lineTotal = unitPrice * item.Quantity;
            totalBeforeDiscount += lineTotal;

            response.Products.Add(new OrderProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Quantity = item.Quantity,
                Price_Per_Unit = product.Price,
                Total = Math.Round(lineTotal, 2)
            });
        }

        // S'il y a des erreurs → on stoppe
        if (errors.Any())
            return (null, errors);

        decimal total = totalBeforeDiscount;

        // Remise commande > 100€
        if (totalBeforeDiscount > 100)
        {
            var discountValue = totalBeforeDiscount * 0.05m;
            response.Discounts.Add(new Discount { Type = "order", Value = Math.Round(discountValue, 2) });
            total -= discountValue;
        }

        // Code promo
        if (!string.IsNullOrEmpty(request.Promo_Code))
        {
            if (totalBeforeDiscount < 50)
            {
                return (null, new List<string>
                {
                    "Les codes promos ne sont valables quà partir de 50€ d'achat"
                });
            }

            decimal promoRate = request.Promo_Code switch
            {
                "DISCOUNT20" => 0.20m,
                "DISCOUNT10" => 0.10m,
                _ => -1
            };

            if (promoRate < 0)
            {
                return (null, new List<string> { "Le code promo est invalide" });
            }

            var promoValue = totalBeforeDiscount * promoRate;
            response.Discounts.Add(new Discount { Type = "promo", Value = Math.Round(promoValue, 2) });
            total -= promoValue;
        }

        response.Total = Math.Round(total, 2);

        // Mise à jour des stocks
        foreach (var item in request.Products)
        {
            var product = products.First(p => p.Id == item.Id);
            product.Stock -= item.Quantity;
        }

        return (response, errors);
    }
}