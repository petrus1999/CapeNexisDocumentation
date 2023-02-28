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
    public class LearnersController : Controller
    {
        private readonly CapeNexisDocumentationContext _context;

        public LearnersController(CapeNexisDocumentationContext context)
        {
            _context = context;
        }

        // GET: Learners
        public async Task<IActionResult> Index(string searchString)
        {

            var Learners = from m in _context.Learner
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                Learners = Learners.Where(s => s.FirName!.Contains(searchString) || s.SurName!.Contains(searchString));
            }

            return View(await Learners.ToListAsync());
        }


        // GET: Learners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Learner == null)
            {
                return NotFound();
            }

            var learner = await _context.Learner
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // GET: Learners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Learners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LearnerId,SurName,FirstName,NationalIdentityNumber,Course,Price")] Learners learner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(learner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(learner);
        }

        // GET: Learners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Learner == null)
            {
                return NotFound();
            }

            var learner = await _context.Learner.FindAsync(id);
            if (learner == null)
            {
                return NotFound();
            }
            return View(learner);
        }

        // POST: Learners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LearnerId,SurName,FirstName,NationalIdentityNumber,Course,Price")] Learners learner)
        {
            if (id != learner.LearnerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(learner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerId))
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
            return View(learner);
        }

        // GET: Learners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Learner == null)
            {
                return NotFound();
            }

            var learner = await _context.Learner
                .FirstOrDefaultAsync(m => m.LearnerId == id);
            if (learner == null)
            {
                return NotFound();
            }

            return View(learner);
        }

        // POST: Learners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Learner == null)
            {
                return Problem("Entity set 'CapeNexisDocumentationContext.Learner'  is null.");
            }
            var learner = await _context.Learner.FindAsync(id);
            if (learner != null)
            {
                _context.Learner.Remove(learner);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LearnerExists(int id)
        {
          return (_context.Learner?.Any(e => e.LearnerId == id)).GetValueOrDefault();
        }
    }
}
