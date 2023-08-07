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
    public class EducationsController : Controller
    {
        private readonly comportfoliowebsiteContext _context;

        public EducationsController(comportfoliowebsiteContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
            var comportfoliowebsiteContext = _context.Education.Include(e => e.Person);
            return View(await comportfoliowebsiteContext.ToListAsync());
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id");
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartYear,EndYear,IsCurrentlyEnrolled,CourseName,Description,CGPA,UniversityName,PersonId")] Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id", education.PersonId);
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var education = await _context.Education.FindAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id", education.PersonId);
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StartYear,EndYear,IsCurrentlyEnrolled,CourseName,Description,CGPA,UniversityName,PersonId")] Education education)
        {
            if (id != education.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.Id))
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
            ViewData["PersonId"] = new SelectList(_context.PersonalInformation, "Id", "Id", education.PersonId);
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Education == null)
            {
                return NotFound();
            }

            var education = await _context.Education
                .Include(e => e.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Education == null)
            {
                return Problem("Entity set 'comportfoliowebsiteContext.Education'  is null.");
            }
            var education = await _context.Education.FindAsync(id);
            if (education != null)
            {
                _context.Education.Remove(education);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
          return (_context.Education?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
