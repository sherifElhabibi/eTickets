using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using eTickets.Models;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class MoviesController : Controller
    {
        private readonly IMovieService _service;

        public MoviesController(IMovieService service)
        {
            _service = service;
        }

        // GET
        [AllowAnonymous]

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll(n => n.Cinema));
        }
        [AllowAnonymous]

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAll(n => n.Cinema);
            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) /*|| n.Description.Contains(searchString)*/).ToList();
                return View("Index", filteredResult);
            }
            return View("Index", allMovies);
        }

        // DETAILS
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            return View(movie);
        }

        //ADD 
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas ?? new List<Cinema>(), "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors ?? new List<Actor>(), "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers ?? new List<Producer>(), "Id", "FullName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM newMovie)
        {
            if(!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas ?? new List<Cinema>(), "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors ?? new List<Actor>(), "Id", "FullName");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers ?? new List<Producer>(), "Id", "FullName");
                return View(newMovie);
            }
            await _service.AddNewMovieAsync(newMovie);
            return RedirectToAction(nameof(Index));
        }

        //EDIT 
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _service.GetMovieByIdAsync(id);
            if(movie == null) return View("NotFound");
            var response = new NewMovieVM()
            {
                Id = movie.Id,
                Name = movie.Name,
                Description = movie.Description,
                Price = movie.Price,
                ImageURL = movie.ImageURL,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                MovieCategory = movie.MovieCategory,
                CinemaId = movie.CinemaId,
                ProducerId = movie.ProducerId,
                ActorsIds = movie.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            var movieDropdownData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas ?? new List<Cinema>(), "Id", "Name");
            ViewBag.Actors = new SelectList(movieDropdownData.Actors ?? new List<Actor>(), "Id", "FullName");
            ViewBag.Producers = new SelectList(movieDropdownData.Producers ?? new List<Producer>(), "Id", "FullName");
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM newMovie)
        {
            if (id != newMovie.Id) { return View("NotFound"); }
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinemas = new SelectList(movieDropdownData.Cinemas ?? new List<Cinema>(), "Id", "Name");
                ViewBag.Actors = new SelectList(movieDropdownData.Actors ?? new List<Actor>(), "Id", "FullName");
                ViewBag.Producers = new SelectList(movieDropdownData.Producers ?? new List<Producer>(), "Id", "FullName");
                return View(newMovie);
            }
            await _service.UpdateMovieAsync(newMovie);
            return RedirectToAction(nameof(Index));
        }

    }
}
