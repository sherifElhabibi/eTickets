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
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{
    [Authorize(Roles =UserRoles.Admin)]
    public class ProducersController : Controller
    {
        private readonly IProducerService _service;

        public ProducersController(IProducerService service)
        {
            _service = service;
        }

        // GET
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }
        // DETAILS
        [AllowAnonymous]

        public async Task<IActionResult> Details(int id)
        {
            var prod = await _service.GetByIdAsync(id);
            if (prod == null) { return View("NotFound"); }
            return View(prod);
        }
        // ADD
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producer prod)
        {
            if (!ModelState.IsValid) { return View(prod); }
            await _service.AddAsync(prod);
            return RedirectToAction(nameof(Index));
        }
        // EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var prod = await _service.GetByIdAsync(id);
            if (prod == null) { return View("NotFound"); }
            return View(prod);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureUrl,FullName,Bio")] Producer prod)
        {
            if (id != prod.Id) { return NotFound(); }
            if (!ModelState.IsValid) { return View(prod); }
            await _service.UpdateAsync(id, prod);
            return RedirectToAction(nameof(Index));
        }

        // DELETE

        public async Task<IActionResult> Delete(int id)
        {
            var prod = await _service.GetByIdAsync(id);
            if (prod == null) return View("Not Found");
            return View(prod);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var prod = await _service.GetByIdAsync(id);
            if (prod != null)
            {
                return View(prod);
            }

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
