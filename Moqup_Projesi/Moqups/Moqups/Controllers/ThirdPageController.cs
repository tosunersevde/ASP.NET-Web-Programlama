using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Moqups.Models;

namespace Moqups.Controllers
{
    public class ThirdPageController : Controller
    {
        
        public IActionResult Index(string takipNo)//url den gönderdiğim takip numarası
        {
            ViewBag.PageName = "Bildirim Sonucu";
            ViewBag.PageNumber = 3;
            var tempDataTakipNo = TempData["takipNo"];//TempDatadan gönderdiğim takip numarası
            return View((object)takipNo);//url den değişken alıyorsan eğer "object" tipine casting yapmalısın "tempDataTakipNo" şeklinde alıyorsan doğrudan gönderebilirsin.
        }
    }
}