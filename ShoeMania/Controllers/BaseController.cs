using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoeMania.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}
