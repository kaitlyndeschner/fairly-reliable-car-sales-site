using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FairlyReliableCarSalesSite.Data;
using FairlyReliableCarSalesSite.Models;

namespace FairlyReliableCarSalesSite.Controllers
{
    public class NewsController : Controller
    {
        private readonly FairlyReliableCarSalesDatabaseContext _context;

        public NewsController(FairlyReliableCarSalesDatabaseContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var newsItems = await _context.NewsItems.ToListAsync();
            return View(newsItems);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("News ID is required.");
            }

            var news = await _context.NewsItems.FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound("The requested news item does not exist.");
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Summary,Details,ImageUrl")] News news)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.NewsItems.AddAsync(news);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "News item created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while creating the news item: {ex.Message}");
                }
            }

            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest("News ID is required.");
            }

            var news = await _context.NewsItems.FindAsync(id);
            if (news == null)
            {
                return NotFound("The requested news item does not exist.");
            }

            return View(news);
        }

        // POST: News/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Summary,Details,ImageUrl")] News news)
        {
            if (id != news.Id)
            {
                return BadRequest("News ID mismatch.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.NewsItems.Update(news);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "News item updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound("The requested news item does not exist.");
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"An error occurred while updating the news item: {ex.Message}");
                }
            }

            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest("News ID is required.");
            }

            var news = await _context.NewsItems.FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound("The requested news item does not exist.");
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.NewsItems.FindAsync(id);
            if (news == null)
            {
                return NotFound("The news item to delete does not exist.");
            }

            try
            {
                _context.NewsItems.Remove(news);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "News item deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the news item: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.NewsItems.Any(e => e.Id == id);
        }
    }
}
