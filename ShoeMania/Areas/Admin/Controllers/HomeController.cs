using Microsoft.AspNetCore.Mvc;

namespace ShoeMania.Areas.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
