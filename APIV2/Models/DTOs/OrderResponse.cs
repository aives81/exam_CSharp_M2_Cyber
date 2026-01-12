namespace exam_CSharp_M2_Cyber.Models;

public class OrderResponse
{
    public List<OrderProductResponse> Products { get; set; } = new();
    public List<Discount> Discounts { get; set; } = new();
    public decimal Total { get; set; }
}