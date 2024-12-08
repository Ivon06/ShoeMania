using Microsoft.AspNetCore.Mvc;
using ShoeMania.Models;
using System.Diagnostics;

namespace ShoeMania.Controllers
{
	public class HomeController : Controller
    {
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Home", new { Area = "Admin" });
            }
            return View();
        }

		public IActionResult Privacy()
		{
			return View();
		}

        public IActionResult WhyUs()
        {
            return View();
        }

  //      [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		//public IActionResult Error()
		//{
		//	return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		//}

        

        public IActionResult Error404()
        {
            string errorMessage = TempData["ErrorMessage"]?.ToString() ?? "An unexpected error occurred.";

            ViewData["ErrorMessage"] = errorMessage;

            return View();
        }

        public IActionResult Error500()
        {
            string errorMessage = TempData["ErrorMessage"]?.ToString() ?? "An unexpected error occurred.";

            ViewData["ErrorMessage"] = errorMessage;

            return View();
        }
    }
}
