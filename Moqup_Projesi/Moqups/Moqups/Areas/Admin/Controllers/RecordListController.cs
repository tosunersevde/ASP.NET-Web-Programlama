using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqups.Models;
using Repositories;

namespace Moqups.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RecordListController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly RepositoryContext _context;

        public RecordListController(RepositoryContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.PageName = 6;
            ViewBag.PageTitle = "List Records";
            var query = from takipNo in _context.TakipNumaralari
                        join messages in _context.Messages on takipNo.Id equals messages.TakipNoId
                        select new GetMessageAndTakipNo
                        {
                            adSoyad = messages.adSoyad,
                            isOk = messages.isOk,
                            takipNoString = takipNo.TakipNumarası,
                        };
            var result = query.ToList();
            return View(result);
        }
    }
}
