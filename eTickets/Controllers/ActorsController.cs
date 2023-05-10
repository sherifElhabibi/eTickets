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
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;
using eTickets.Data.Static;

namespace eTickets.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }


        // GET
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        //GET
        [AllowAnonymous]

        public async Task<IActionResult> Details(int? id)
        {
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) { return View("NotFound"); };
            return View(actor);
        }

        // POST
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _service.GetByIdAsync(id);
            if (actor == null) return View("NotFound");
            return View(actor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (id != actor.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }
    }
    // DELETE

    //public async Task<IActionResult> Delete(int id)
    //{
    //    var cinema = await _service.GetByIdAsync(id);
    //    if (cinema == null) return View("Not Found");
    //    return View(cinema);
    //}

    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirm(int id)
    //{
    //    var cinema = await _service.GetByIdAsync(id);
    //    if (cinema != null)
    //    {
    //        return View(cinema);
    //    }

    //    await _service.DeleteAsync(id);
    //    return RedirectToAction(nameof(Index));
    //}

}



