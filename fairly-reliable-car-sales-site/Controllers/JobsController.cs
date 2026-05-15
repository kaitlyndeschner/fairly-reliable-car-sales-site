using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FairlyReliableCarSalesSite.Data;
using FairlyReliableCarSalesSite.Models;

namespace FairlyReliableCarSalesSite.Controllers
{
    public class JobsController : Controller
    {
        private readonly FairlyReliableCarSalesDatabaseContext _context;

        public JobsController(FairlyReliableCarSalesDatabaseContext context)
        {
            _context = context;
        }

        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return View(jobs);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Job ID is required.");
            }

            var job = await _context.Jobs.FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound("The requested job does not exist.");
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Jobs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,ImageUrl")] Job job)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.Jobs.AddAsync(job);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Job created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the job: {ex.Message}");
                }
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("Job ID is required.");
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound("The requested job does not exist.");
            }

            return View(job);
        }

        // POST: Jobs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageUrl")] Job job)
        {
            if (id != job.Id)
            {
                return BadRequest("Job ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Jobs.Update(job);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Job updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound("The requested job does not exist.");
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the job: {ex.Message}");
                }
            }

            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("Job ID is required.");
            }

            var job = await _context.Jobs.FirstOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound("The requested job does not exist.");
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound("The job to delete does not exist.");
            }

            try
            {
                _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Job deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the job: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.Id == id);
        }
    }
}
