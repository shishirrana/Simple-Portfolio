using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using com.portfolio.website.Data;
using com.portfolio.website.Models;
using com.portfolio.website.Filters;

namespace com.portfolio.website.Controllers
{
    public class PersonalInformationsController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        private readonly comportfoliowebsiteContext _context;

        public PersonalInformationsController(comportfoliowebsiteContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment=hostingEnvironment;
        }

        // GET: PersonalInformations
        public async Task<IActionResult> Index()
        {
              return _context.PersonalInformation != null ? 
                          View(await _context.PersonalInformation.ToListAsync()) :
                          Problem("Entity set 'comportfoliowebsiteContext.PersonalInformation'  is null.");
        }

        // GET: PersonalInformations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonalInformation == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PersonalInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        // GET: PersonalInformations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PersonalInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Highlight,Summary,CurrentRole,Email,DOB,Phone,Skype,Facebook,Twitter,Google,Instagram,GitHub,Address,Photo")] PersonalInformation personalInformation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personalInformation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personalInformation);
        }

        // GET: PersonalInformations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonalInformation == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PersonalInformation.FindAsync(id);
            if (personalInformation == null)
            {
                return NotFound();
            }
            return View(personalInformation);
        }

        // POST: PersonalInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Highlight,Summary,CurrentRole,Email,DOB,Phone,Skype,Facebook,Twitter,Google,Instagram,GitHub,Address,Photo")] PersonalInformation personalInformation, List<IFormFile> Photo)
        {
            if (id != personalInformation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(Request.Form.Files.Count> 0)
                    {
                        // save files

                        var files = Request.Form.Files;
                        var file = files[0];

                        personalInformation.Photo = saveFileDir(file);
                    }
                    _context.Update(personalInformation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonalInformationExists(personalInformation.Id))
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
            return View(personalInformation);
        }

        private string? saveFileDir(IFormFile file)
        {
            // finding root directory -> upto wwwroot folder
            // combine root directory with uploads folder
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (file.Length > 0)
                {
                    //creating full absolute path for saving images
                    string filePath = Path.Combine(uploads, file.FileName);

                    //for displaying image in html img element
                    string relative = "/uploads/" + file.FileName;

                    //a file stream is creating to write existing file in given directory (filepath)
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        // Existing file is copied to given directory of file
                         file.CopyTo(fileStream);
                    }
                    return filePath;
                }
            return "";
            
        }

        // GET: PersonalInformations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonalInformation == null)
            {
                return NotFound();
            }

            var personalInformation = await _context.PersonalInformation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personalInformation == null)
            {
                return NotFound();
            }

            return View(personalInformation);
        }

        // POST: PersonalInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonalInformation == null)
            {
                return Problem("Entity set 'comportfoliowebsiteContext.PersonalInformation'  is null.");
            }
            var personalInformation = await _context.PersonalInformation.FindAsync(id);
            if (personalInformation != null)
            {
                _context.PersonalInformation.Remove(personalInformation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonalInformationExists(int id)
        {
          return (_context.PersonalInformation?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
