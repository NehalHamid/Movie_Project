using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Movie_Project.Data;
using Movie_Project.Models;

namespace Movie_Project.Controllers
{
    public class GenerController : Controller
    {
        private readonly MovieDBContext _context;

        public GenerController(MovieDBContext context)
        {
            _context = context;
        }

        // GET: Gener
        public async Task<IActionResult> Index()
        {
              return _context.Geners != null ? 
                          View(await _context.Geners.ToListAsync()) :
                          Problem("Entity set 'MovieDBContext.Geners'  is null.");
        }

        // GET: Gener/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Geners == null)
            {
                return NotFound();
            }

            var gener = await _context.Geners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gener == null)
            {
                return NotFound();
            }

            return View(gener);
        }

        // GET: Gener/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gener/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,genre")] Gener gener)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gener);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gener);
        }

        // GET: Gener/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Geners == null)
            {
                return NotFound();
            }

            var gener = await _context.Geners.FindAsync(id);
            if (gener == null)
            {
                return NotFound();
            }
            return View(gener);
        }

        // POST: Gener/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,genre")] Gener gener)
        {
            if (id != gener.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gener);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenerExists(gener.Id))
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
            return View(gener);
        }

        // GET: Gener/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Geners == null)
            {
                return NotFound();
            }

            var gener = await _context.Geners
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gener == null)
            {
                return NotFound();
            }

            return View(gener);
        }

        // POST: Gener/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Geners == null)
            {
                return Problem("Entity set 'MovieDBContext.Geners'  is null.");
            }
            var gener = await _context.Geners.FindAsync(id);
            if (gener != null)
            {
                _context.Geners.Remove(gener);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenerExists(int id)
        {
          return (_context.Geners?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
