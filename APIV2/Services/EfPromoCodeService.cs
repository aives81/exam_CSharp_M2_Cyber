using exam_CSharp_M2_Cyber.Data;

namespace exam_CSharp_M2_Cyber.Services
{
    public class EfPromoCodeService : IPromoCodeService
    {
        private readonly ECommerceDbContext _db;

        public EfPromoCodeService(ECommerceDbContext db)
        {
            _db = db;
        }

        public decimal? GetDiscountRate(string code)
        {
            var promo = _db.PromoCodes
                .FirstOrDefault(p => p.Code == code);

            return promo?.DiscountRate;
        }
    }
}