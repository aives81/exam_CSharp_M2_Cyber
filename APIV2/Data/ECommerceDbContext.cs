using exam_CSharp_M2_Cyber.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace exam_CSharp_M2_Cyber.Data
{
    public class ECommerceDbContext : DbContext
    {
        public ECommerceDbContext(DbContextOptions<ECommerceDbContext> options)
            : base(options)
        {
        }

        public DbSet<ProductEntity> Products => Set<ProductEntity>();
        public DbSet<PromoCodeEntity> PromoCodes => Set<PromoCodeEntity>();
    }
}