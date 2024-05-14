using Microsoft.AspNetCore.Mvc;

namespace Moqups.Controllers
{
    public class FirstPageController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.PageName = "Karşılama";
            ViewBag.PageNumber = 1;
            return View();
        }
    }
}
