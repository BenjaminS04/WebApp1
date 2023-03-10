using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp1.Data;
using WebApp1.Models;

namespace WebApp1.Controllers
{
    public class peopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public peopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: people
        public async Task<IActionResult> Index()
        {
              return View(await _context.person.ToListAsync());
        }

        // GET: people/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.person == null)
            {
                return NotFound();
            }

            var person = await _context.person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: people/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: people/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,surname")] person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: people/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.person == null)
            {
                return NotFound();
            }

            var person = await _context.person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: people/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,surname")] person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!personExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: people/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.person == null)
            {
                return NotFound();
            }

            var person = await _context.person
                .FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: people/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.person == null)
            {
                return Problem("Entity set 'ApplicationDbContext.person'  is null.");
            }
            var person = await _context.person.FindAsync(id);
            if (person != null)
            {
                _context.person.Remove(person);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool personExists(int id)
        {
          return _context.person.Any(e => e.Id == id);
        }
    }
}
