using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CapeNexisDocumentation.Data;
using CapeNexisDocumentation.Models;

namespace CapeNexisDocumentation.Controllers
{
    public class facilitatorsController : Controller
    {
        private readonly CapeNexisDocumentationContext _context;

        public facilitatorsController(CapeNexisDocumentationContext context)
        {
            _context = context;
        }

        // GET: Facilitators
        public async Task<IActionResult> Index(string searchString)
        {

            var facilitators = from m in _context.facilitator
                               select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                facilitators = facilitators.Where(s => s.FirstName!.Contains(searchString) || s.SurName!.Contains(searchString));
            }

            return View(await facilitators.ToListAsync());
        }

        // GET: facilitators/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.facilitator == null)
            {
                return NotFound();
            }

            var facilitator = await _context.facilitator
                .FirstOrDefaultAsync(m => m.facilitatorId == id);
            if (facilitator == null)
            {
                return NotFound();
            }

            return View(facilitator);
        }

        // GET: facilitators/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: facilitators/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("facilitatorId,SurName,FirstName,NationalIdentityNumber,NumberOfLearners")] facilitators facilitator)
        {
            if (ModelState.IsValid)
            {
                _context.Add(facilitator);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(facilitator);
        }

        // GET: facilitators/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.facilitator == null)
            {
                return NotFound();
            }

            var facilitator = await _context.facilitator.FindAsync(id);
            if (facilitator == null)
            {
                return NotFound();
            }
            return View(facilitator);
        }

        // POST: facilitators/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("facilitatorId,SurName,FirstName,NationalIdentityNumber,NumberOfLearners")] facilitators facilitator)
        {
            if (id != facilitator.facilitatorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facilitator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!facilitatorExists(facilitator.facilitatorId))
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
            return View(facilitator);
        }

        // GET: facilitators/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.facilitator == null)
            {
                return NotFound();
            }

            var facilitator = await _context.facilitator
                .FirstOrDefaultAsync(m => m.facilitatorId == id);
            if (facilitator == null)
            {
                return NotFound();
            }

            return View(facilitator);
        }

        // POST: facilitators/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.facilitator == null)
            {
                return Problem("Entity set 'CapeNexisDocumentationContext.facilitator'  is null.");
            }
            var facilitator = await _context.facilitator.FindAsync(id);
            if (facilitator != null)
            {
                _context.facilitator.Remove(facilitator);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool facilitatorExists(int id)
        {
          return (_context.facilitator?.Any(e => e.facilitatorId == id)).GetValueOrDefault();
        }
    }
}
