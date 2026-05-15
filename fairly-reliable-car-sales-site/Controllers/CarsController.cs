using FairlyReliableCarSalesSite.Data;
using FairlyReliableCarSalesSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FairlyReliableCarSalesSite.Controllers
{
    public class CarsController : Controller
    {
        private readonly FairlyReliableCarSalesDatabaseContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CarsController(FairlyReliableCarSalesDatabaseContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Cars
        public async Task<IActionResult> Index(
            string searchMake, string searchModel, string searchFuelType,
            string searchColour, int? yearMin, int? yearMax, decimal? minPrice, decimal? maxPrice)
        {
            var cars = _context.Cars.AsQueryable();

            // Filters
            if (!string.IsNullOrEmpty(searchMake))
                cars = cars.Where(c => c.Make.Contains(searchMake));

            if (!string.IsNullOrEmpty(searchModel))
                cars = cars.Where(c => c.Model.Contains(searchModel));

            if (!string.IsNullOrEmpty(searchFuelType))
                cars = cars.Where(c => c.FuelType.Contains(searchFuelType));

            if (!string.IsNullOrEmpty(searchColour))
                cars = cars.Where(c => c.Colour.Contains(searchColour));

            if (yearMin.HasValue)
                cars = cars.Where(c => c.Year >= yearMin.Value);

            if (yearMax.HasValue)
                cars = cars.Where(c => c.Year <= yearMax.Value);

            if (minPrice.HasValue)
                cars = cars.Where(c => c.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                cars = cars.Where(c => c.Price <= maxPrice.Value);

            return View(await cars.ToListAsync());
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Year,Make,Model,Colour,FuelType,Price,Details,ImageFile")] Car car)
        {
            if (ModelState.IsValid)
            {
                if (car.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(car.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await car.ImageFile.CopyToAsync(fileStream);
                    }

                    car.ImageUrl = "/images/" + uniqueFileName;
                }
                else
                {
                    car.ImageUrl = "/images/default.jpg";
                }

                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.FindAsync(id);
            if (car == null) return NotFound();

            return View(car);
        }

        // POST: Cars/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,Year,Make,Model,Colour,FuelType,Price,Details,ImageFile,ImageUrl")] Car car)
        {
            if (id != car.CarId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    if (car.ImageFile != null)
                    {
                        string uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, "images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(car.ImageFile.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await car.ImageFile.CopyToAsync(fileStream);
                        }

                        car.ImageUrl = "/images/" + uniqueFileName;
                    }

                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Cars.Any(e => e.CarId == car.CarId)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var car = await _context.Cars.FirstOrDefaultAsync(m => m.CarId == id);
            if (car == null) return NotFound();

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
