using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly eTicketContext _context;
        public OrdersService(eTicketContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Order>> GetOrderByUserIdAsync(string userId)
        {
            var orders = await _context.Order
                .Include(n => n.OrderItems)
                .ThenInclude(n => n.Movie)
                .Where(n => n.UserId == userId).ToListAsync();
            return orders;
        }

        public async Task StoreOrder(ICollection<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();
            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
                await _context.OrderItem.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
