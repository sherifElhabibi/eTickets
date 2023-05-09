﻿using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Cart
{
    public class ShoppingCart
    {
        eTicketContext _context;
        public ShoppingCart(eTicketContext context)
        {

            _context = context;

        }
        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = services.GetService<eTicketContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public string ShoppingCartId { get; set; }
        public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ICollection<ShoppingCartItem> GetShoppingCartItems() => ShoppingCartItems ?? (ShoppingCartItems = _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Include(n => n.Movie).ToList());

        public double GetShoppingCartTotal() => _context.ShoppingCartItem.Where(n => n.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();

        public void AddItemToCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };
                _context.ShoppingCartItem.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public void RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = _context.ShoppingCartItem.FirstOrDefault(n => n.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItem.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();

        }

        public async Task ClearShoppingCartAsync()
        {
            var items = await _context.ShoppingCartItem.Where(n=>n.ShoppingCartId == ShoppingCartId).ToListAsync();
            _context.ShoppingCartItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }

    }
}
