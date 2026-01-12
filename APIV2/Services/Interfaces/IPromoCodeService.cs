namespace exam_CSharp_M2_Cyber.Services
{
    public interface IPromoCodeService
    {
        decimal? GetDiscountRate(string code);
    }
}