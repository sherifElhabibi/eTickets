using eTickets.Models;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        eTicketContext _context;
        public ShoppingCart(eTicketContext context)
        {

            _context = context; 

        }
        public string ShoppingCartId { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } 
        
    }
}
