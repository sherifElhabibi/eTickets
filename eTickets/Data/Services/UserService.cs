//using eTickets.Models;
//using Microsoft.AspNetCore.Authentication.Cookies;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Security.Claims;

//namespace eTickets.Data.Services
//{
//    public class UserService : IUserService
//    {
//        private readonly eTicketContext _context;
//        public UserService(eTicketContext context)
//        {
//            _context = context;
//        }

//        public async Task<User> LoginAsync(Login user, string returnUrl)
//        {
//            var succeeded = await _context.User.Include(a=>a.Roles).FirstOrDefaultAsync(authUser => authUser.Email == user.Email && authUser.Password == user.Password);
//            return succeeded;

//        }

//        public async Task RegisterAsync(User user)
//        {
//            await _context.User.AddAsync(user);
//            await _context.SaveChangesAsync();
//        }
//    }
//}
