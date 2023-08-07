using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using com.portfolio.website.Data;
using com.portfolio.website.Models;

namespace com.portfolio.website.Controllers
{
    public class MyExpertisesController : Controller
    {
        private readonly comportfoliowebsiteContext _context;

        public MyExpertisesController(comportfoliowebsiteContext context)
        {
            _context = context;
        }

        // GET: MyExpertises
        public async Task<IActionResult> Index()
        {
            var comportfoliowebsiteContext = _context.MyExpertise.Include(m => m.Person);
            return View(await comportfoliowebsiteContext.ToListAsync());
        }

        // GET: MyExpertises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MyExpertise == null)
            {
                return NotFound();
            }

            var myExpertise = await _context.MyExpertise
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myExpertise == null)
            {
                return NotFound();
            }

            return View(myExpertise);
        }

        // GET: MyExpertises/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id");
            return View();
        }

        // POST: MyExpertises/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,PersonId")] MyExpertise myExpertise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(myExpertise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id", myExpertise.PersonId);
            return View(myExpertise);
        }

        // GET: MyExpertises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MyExpertise == null)
            {
                return NotFound();
            }

            var myExpertise = await _context.MyExpertise.FindAsync(id);
            if (myExpertise == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id", myExpertise.PersonId);
            return View(myExpertise);
        }

        // POST: MyExpertises/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PersonId")] MyExpertise myExpertise)
        {
            if (id != myExpertise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(myExpertise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MyExpertiseExists(myExpertise.Id))
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
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id", myExpertise.PersonId);
            return View(myExpertise);
        }

        // GET: MyExpertises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MyExpertise == null)
            {
                return NotFound();
            }

            var myExpertise = await _context.MyExpertise
                .Include(m => m.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (myExpertise == null)
            {
                return NotFound();
            }

            return View(myExpertise);
        }

        // POST: MyExpertises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MyExpertise == null)
            {
                return Problem("Entity set 'comportfoliowebsiteContext.MyExpertise'  is null.");
            }
            var myExpertise = await _context.MyExpertise.FindAsync(id);
            if (myExpertise != null)
            {
                _context.MyExpertise.Remove(myExpertise);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MyExpertiseExists(int id)
        {
          return (_context.MyExpertise?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
