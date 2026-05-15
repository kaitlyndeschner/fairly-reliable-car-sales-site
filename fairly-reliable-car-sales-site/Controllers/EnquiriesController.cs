using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FairlyReliableCarSalesSite.Data;
using FairlyReliableCarSalesSite.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FairlyReliableCarSalesSite.Controllers
{
    public class EnquiriesController : Controller
    {
        private readonly FairlyReliableCarSalesDatabaseContext _context;

        public EnquiriesController(FairlyReliableCarSalesDatabaseContext context)
        {
            _context = context;
        }

        // GET: Enquiries
        public async Task<IActionResult> Index(string searchName, string searchEmail, string searchPhone, int? searchCarId, string searchMessage)
        {
            var enquiries = _context.Enquiries.Include(e => e.Car).AsQueryable(); // Include Car details

            // Apply filters
            if (!string.IsNullOrEmpty(searchName))
                enquiries = enquiries.Where(e => e.Name.Contains(searchName));

            if (!string.IsNullOrEmpty(searchEmail))
                enquiries = enquiries.Where(e => e.Email.Contains(searchEmail));

            if (!string.IsNullOrEmpty(searchPhone))
                enquiries = enquiries.Where(e => e.PhoneNumber.Contains(searchPhone));

            if (searchCarId.HasValue)
                enquiries = enquiries.Where(e => e.CarId == searchCarId);

            if (!string.IsNullOrEmpty(searchMessage))
                enquiries = enquiries.Where(e => e.Message.Contains(searchMessage));

            // Pass current filters back to ViewData for maintaining form values
            ViewData["searchName"] = searchName;
            ViewData["searchEmail"] = searchEmail;
            ViewData["searchPhone"] = searchPhone;
            ViewData["searchCarId"] = searchCarId;
            ViewData["searchMessage"] = searchMessage;

            return View(await enquiries.ToListAsync());
        }

        // GET: Enquiries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiries
                .Include(e => e.Car) // Include Car details
                .FirstOrDefaultAsync(m => m.EnquiryId == id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return View(enquiry);
        }

        // GET: Enquiries/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId");
            return View();
        }

        // POST: Enquiries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnquiryId,Name,PhoneNumber,Email,CarId,Message,DateSubmitted")] Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enquiry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", enquiry.CarId);
            return View(enquiry);
        }

        // GET: Enquiries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", enquiry.CarId);
            return View(enquiry);
        }

        // POST: Enquiries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnquiryId,Name,PhoneNumber,Email,CarId,Message,DateSubmitted")] Enquiry enquiry)
        {
            if (id != enquiry.EnquiryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enquiry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnquiryExists(enquiry.EnquiryId))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "CarId", "CarId", enquiry.CarId);
            return View(enquiry);
        }

        // GET: Enquiries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enquiry = await _context.Enquiries
                .Include(e => e.Car) // Include Car details
                .FirstOrDefaultAsync(m => m.EnquiryId == id);
            if (enquiry == null)
            {
                return NotFound();
            }

            return View(enquiry);
        }

        // POST: Enquiries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var enquiry = await _context.Enquiries.FindAsync(id);
            if (enquiry != null)
            {
                _context.Enquiries.Remove(enquiry);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnquiryExists(int id)
        {
            return _context.Enquiries.Any(e => e.EnquiryId == id);
        }
    }
}
