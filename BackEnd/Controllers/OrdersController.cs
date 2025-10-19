using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GroceryStoreAPI.Data;
using GroceryStoreAPI.DTOs;
using GroceryStoreAPI.Models;

namespace GroceryStoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly GroceryStoreContext _context;

        public OrdersController(GroceryStoreContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> CreateOrder(CreateOrderDto createOrderDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            var order = new Order
            {
                UserId = userId,
                DeliverySlot = createOrderDto.DeliverySlot,
                PaymentMethod = createOrderDto.PaymentMethod,
                Status = "Completed",
                PaymentStatus = "Completed"
            };

            decimal totalAmount = 0;
            foreach (var itemDto in createOrderDto.Items)
            {
                var groceryItem = await _context.GroceryItems.FindAsync(itemDto.GroceryItemId);
                if (groceryItem == null || groceryItem.Quantity < itemDto.Quantity)
                    return BadRequest($"Item {itemDto.GroceryItemId} not available in requested quantity");

                var orderItem = new OrderItem
                {
                    GroceryItemId = itemDto.GroceryItemId,
                    Quantity = itemDto.Quantity,
                    Price = groceryItem.Price
                };

                order.OrderItems.Add(orderItem);
                totalAmount += groceryItem.Price * itemDto.Quantity;
                
                groceryItem.Quantity -= itemDto.Quantity;
            }

            order.TotalAmount = totalAmount;
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Ok(await GetOrderResponse(order.Id));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetUserOrders()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            
            var orders = await _context.Orders
                .Where(o => o.UserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.GroceryItem)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();

            return Ok(orders.Select(MapToOrderResponse));
        }

        [HttpGet("delivery-slots")]
        public ActionResult<IEnumerable<DateTime>> GetDeliverySlots()
        {
            var slots = new List<DateTime>();
            var today = DateTime.Today;
            
            for (int i = 1; i <= 7; i++)
            {
                var date = today.AddDays(i);
                slots.Add(date.AddHours(9));  // 9 AM
                slots.Add(date.AddHours(14)); // 2 PM
                slots.Add(date.AddHours(18)); // 6 PM
            }

            return Ok(slots);
        }

        [HttpGet("test")]
        [AllowAnonymous]
        public async Task<ActionResult> TestOrders()
        {
            var orderCount = await _context.Orders.CountAsync();
            var userCount = await _context.Users.CountAsync();
            return Ok(new { OrderCount = orderCount, UserCount = userCount });
        }

        private async Task<OrderResponseDto> GetOrderResponse(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.GroceryItem)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            return MapToOrderResponse(order!);
        }

        private OrderResponseDto MapToOrderResponse(Order order)
        {
            return new OrderResponseDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                DeliverySlot = order.DeliverySlot,
                PaymentMethod = order.PaymentMethod,
                PaymentStatus = order.PaymentStatus,
                CreatedAt = order.CreatedAt,
                Items = order.OrderItems.Select(oi => new OrderItemResponseDto
                {
                    Id = oi.Id,
                    ItemName = oi.GroceryItem.Name,
                    Quantity = oi.Quantity,
                    Price = oi.Price,
                    Total = oi.Price * oi.Quantity
                }).ToList()
            };
        }
    }
}