using OrderApi.Dto;
using OrderApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace OrderApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateProductDto dto)
        {
            var data = await _orderService.CreateOrderAsync(dto.Id);
            return Ok(data);
        }
    }
}
