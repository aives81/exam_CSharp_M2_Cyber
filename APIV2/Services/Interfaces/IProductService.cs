using exam_CSharp_M2_Cyber.Models;

namespace exam_CSharp_M2_Cyber.Services;

public interface IProductService
{
    IReadOnlyList<Product> GetProducts();
}