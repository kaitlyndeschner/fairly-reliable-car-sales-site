using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FairlyReliableCarSalesSite.Data;
using FairlyReliableCarSalesSite.Models;
using FairlyReliableCarSalesSite.ViewModels;

namespace FairlyReliableCarSalesSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly FairlyReliableCarSalesDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, FairlyReliableCarSalesDatabaseContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Default home page
        public IActionResult Index()
        {
            return View();
        }

        // About Us page
        public IActionResult AboutUs()
        {
            return View();
        }

        // Services page
        public IActionResult Services()
        {
            return View();
        }

        // News and Events page
        public IActionResult NewsEvents()
        {
            // Create a new ViewModel
            var viewModel = new NewsEventsViewModel();

            try
            {
                // Fetch data from the database safely, handle nulls
                viewModel.News = _context.NewsItems?.ToList() ?? new List<News>();
                viewModel.Events = _context.Events?.ToList() ?? new List<Event>();
                viewModel.Jobs = _context.Jobs?.ToList() ?? new List<Job>();
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                _logger.LogError(ex, "Error fetching data for NewsEvents ViewModel");

                // Provide user feedback and fallback data
                ViewBag.ErrorMessage = "There was an issue loading the data. Please try again later.";
                viewModel.News = new List<News>();
                viewModel.Events = new List<Event>();
                viewModel.Jobs = new List<Job>();
            }

            // Return the view with the ViewModel
            return View(viewModel);
        }

        // Contact Us page
        public IActionResult ContactUs()
        {
            return View();
        }

        // Error handling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
