using Microsoft.AspNetCore.Mvc;

namespace Moqups.Controllers
{
    public class FourthPageController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.PageName = "Takip";
            ViewBag.PageNumber = 4;
            return View();
        }
    }
}
