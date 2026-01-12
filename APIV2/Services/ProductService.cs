using exam_CSharp_M2_Cyber.Models;

namespace exam_CSharp_M2_Cyber.Services;

public class ProductService: IProductService
{
    private readonly List<Product> _products;
    
    public ProductService()
    {
        // Liste fixe en mémoire
        _products = new List<Product>
        {
            new Product { Id = 1, Name = "Produit A", Price = 10, Stock = 20 },
            new Product { Id = 2, Name = "Produit B", Price = 15, Stock = 5 },
            new Product { Id = 3, Name = "Produit C", Price = 8, Stock = 50 },
            new Product { Id = 4, Name = "Produit D", Price = 25, Stock = 3 },
            new Product { Id = 5, Name = "Produit E", Price = 12, Stock = 10 }
        };
    }
    
    public IReadOnlyList<Product> GetProducts()
    {
        return _products;
    }
}