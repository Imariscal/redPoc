using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RedPoc.Entity.Entities;
using RedPoc.Entity.Viewmodels;
using RedPoc.Service.Interfaces;

namespace RedPoc.Api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService,
                                 ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetOrders")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrdersDetailsAsync()
        {
            _logger.LogInformation($"OrderController.GetOrdersDetislAsync action executed at {DateTime.UtcNow.ToLongTimeString()}");
            var employees = await _orderService.GetOrdersAsync();
            return Ok(employees);
        }

        [HttpGet]
        [Route("GetOrders/ByType")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrdersDetailsAsync(OrderType orderType)
        {
            _logger.LogInformation($"OrderController.GetOrdersDetislAsync action executed at {DateTime.UtcNow.ToLongTimeString()}");
            var employees = await _orderService.GetOrdersByOrderTypeAsync(orderType);
            return Ok(employees);
        }


        [HttpDelete]
        [Route("Delete")]
        public async Task<OrderViewModel> DeleteOrderAsync(Guid orderId)
        {
            _logger.LogInformation($"OrderController.DeleteOrderAsync  action executed at {DateTime.UtcNow.ToLongTimeString()}");
            return await _orderService.DeleteOrderAsync(orderId);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<OrderViewModel> AddOrderAsync([FromBody] AddOrderViewModel orderViewModel)
        {
            _logger.LogInformation($"OrderController.AddOrderAsync  action executed at {DateTime.UtcNow.ToLongTimeString()}");
            return await _orderService.AddOrderAsync(orderViewModel);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<OrderViewModel> UpdateOrderAsync([FromBody] UpdateOrderViewModel orderViewModel)
        {
            _logger.LogInformation($"OrderController.UpdateOrderAsync  action executed at {DateTime.UtcNow.ToLongTimeString()}");
            return await _orderService.UpdateOrderAsync(orderViewModel);
        }
    }
}
