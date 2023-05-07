using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Models;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly eTicketContext _context;

        public MoviesController(eTicketContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            var eTicketContext = _context.Movie.Include(m => m.Cinema).Include(m => m.MovieProducer).OrderBy(n=>n.Name);
            return View(await eTicketContext.ToListAsync());
        }
    }
}
