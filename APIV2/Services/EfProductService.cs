using exam_CSharp_M2_Cyber.Data;
using exam_CSharp_M2_Cyber.Models;

namespace exam_CSharp_M2_Cyber.Services
{
    public class EfProductService : IProductService
    {
        private readonly ECommerceDbContext _db;

        public EfProductService(ECommerceDbContext db)
        {
            _db = db;
        }

        public IReadOnlyList<Product> GetProducts()
        {
            return _db.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Stock = p.Stock
                })
                .ToList();
        }

        public Product? GetById(int id)
        {
            var entity = _db.Products.FirstOrDefault(p => p.Id == id);
            if (entity == null) return null;

            return new Product
            {
                Id = entity.Id,
                Name = entity.Name,
                Price = entity.Price,
                Stock = entity.Stock
            };
        }

        public void UpdateStock(int productId, int quantity)
        {
            var entity = _db.Products.First(p => p.Id == productId);
            entity.Stock -= quantity;
            _db.SaveChanges();
        }
    }
}