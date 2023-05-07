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

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemaService _service;

        public CinemasController(ICinemaService service)
        {
            _service = service;
        }

        // GET

        public async Task<IActionResult> Index()
        {
              return View(await _service.GetAll());
        }

        // DETAILS

        public async Task<IActionResult> Details(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) { return View("NotFound"); }
            return View(cinema);
        }

        // ADD

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid) { return View(cinema); }
            await _service.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        // EDIT

        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) { return View("NotFound"); }
            return View(cinema);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (id != cinema.Id) { return NotFound(); }
            if (!ModelState.IsValid) { return View(cinema); }
            await _service.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }
        // DELETE

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema == null) return View("Not Found");
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var cinema = await _service.GetByIdAsync(id);
            if (cinema != null)
            {
                return View(cinema);
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
