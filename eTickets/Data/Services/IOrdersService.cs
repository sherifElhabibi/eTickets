using eTickets.Models;

namespace eTickets.Data.Services
{
    public interface IOrdersService
    {
        public Task StoreOrder(ICollection<ShoppingCartItem> items, string userId, String userEmailAddress);
        public Task <ICollection<Order>> GetOrderByUserIdAsync(string userId);
        
    }
}
