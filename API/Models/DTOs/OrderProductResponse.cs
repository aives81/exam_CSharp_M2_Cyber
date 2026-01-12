namespace exam_CSharp_M2_Cyber.Models;

public class OrderProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal Price_Per_Unit { get; set; }
    public decimal Total { get; set; }
}