namespace exam_CSharp_M2_Cyber.Models;

public class OrderRequest
{
    public List<OrderProductRequest> Products { get; set; }
    public string? Promo_Code { get; set; }
}