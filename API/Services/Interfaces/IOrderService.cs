using exam_CSharp_M2_Cyber.Models;

namespace exam_CSharp_M2_Cyber.Services.Interfaces;

public interface IOrderService
{
    (OrderResponse? response, List<string> errors) CreateOrder(OrderRequest request);
}