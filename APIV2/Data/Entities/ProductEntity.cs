namespace exam_CSharp_M2_Cyber.Data.Entities;

public class ProductEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }
}