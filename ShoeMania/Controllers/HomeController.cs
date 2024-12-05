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

        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return View("Error404");
            }

            if (statusCode == 401)
            {
                return View("Error401");
            }

            return View();
        }
    }
}
