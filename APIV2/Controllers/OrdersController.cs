using exam_CSharp_M2_Cyber.Models;
using exam_CSharp_M2_Cyber.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace exam_CSharp_M2_Cyber.Controllers
{
    [ApiController]
    [Route("orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult CreateOrder(OrderRequest request)
        {
            var (response, errors) = _orderService.CreateOrder(request);

            if (errors.Any())
            {
                return BadRequest(new { errors });
            }

            return Ok(response);
        }
    }
}