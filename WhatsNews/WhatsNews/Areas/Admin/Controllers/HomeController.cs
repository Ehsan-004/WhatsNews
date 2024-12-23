using Microsoft.AspNetCore.Mvc;

namespace WhatsNews.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
        [Area("Admin")]
        public IActionResult Index()
        {
                return View();
        }
}