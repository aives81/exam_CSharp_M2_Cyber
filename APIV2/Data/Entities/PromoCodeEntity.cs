namespace exam_CSharp_M2_Cyber.Data.Entities;

public class PromoCodeEntity
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public decimal DiscountRate { get; set; }
}