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

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        // GET: Actors
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll());
        }

        //GET: Actors/Details/5
           public async Task<IActionResult> Details(int? id)
           {
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) { return View("NotFound"); };
             return View(actor);
           }

        // GET: Actors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
                await _service.AddAsync(actor);
                return RedirectToAction(nameof(Index));
        }

        // GET: Actors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var actor = await _service.GetByIdAsync(id.Value);
            if (actor == null) return View("NotFound");
            return View(actor);
        }

        // POST: Actors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfilePictureUrl,FullName,Bio")] Actor actor)
        {
            if (id != actor.ActorId)
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
            //return View(actor);
        }

    //GET: Actors/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //var actor = await _service.GetByIdAsync(id.Value);
        //    if (actor == null) return View("Not Found");
        //    return View(actor);
        //} 

        // POST: Actors/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Actor == null)
        //    {
        //        return Problem("Entity set 'eTicketContext.Actor'  is null.");
        //    }
        //    var actor = await _context.Actor.FindAsync(id);
        //    if (actor != null)
        //    {
        //        _context.Actor.Remove(actor);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ActorExists(int id)
        //{
        //  return _context.Actor.Any(e => e.ActorId == id);
        //}
        }
    


