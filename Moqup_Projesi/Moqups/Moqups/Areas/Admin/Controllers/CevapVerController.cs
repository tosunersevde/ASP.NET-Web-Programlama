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
    public class CevapVerController : Controller
    {
        private readonly ILogger<CevapVerController> _logger;
        private readonly IConfiguration _configuration;

        public CevapVerController(ILogger<CevapVerController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult Index(string takipNo)
        {
            TempData["currentTakipNo"] = takipNo;
            ViewBag.PageName = 7;
            ViewBag.PageTitle = "Cevap Ver";
            return View((object)takipNo);
        }
        public async Task<IActionResult> TakipNumarasınaMesajGönder(CallbackMessage model)
        {
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
            using (var dbContext = new RepositoryContext(optionsBuilder.Options))
            {
                // GeriDonusMesaj kolonu ile eşleşen bir TakipNumaralari satırı bulundu.
                var takipNumarasiSatiri = await dbContext.TakipNumaralari
                    .FirstOrDefaultAsync(t => t.TakipNumarası == TempData["currentTakipNo"]);

                if (takipNumarasiSatiri != null)
                {
                    // Eşleşen satırin Id değerini alindi.
                    int takipNumarasiId = takipNumarasiSatiri.Id;
                    model.TakipNoId = takipNumarasiId;
                    var message = await dbContext.Messages
                    .FirstOrDefaultAsync(t => t.TakipNoId == takipNumarasiId);
                    dbContext.CallbackMessages.Add(model);
                    message.isOk = true;
                    dbContext.Update(message);
                    // Id değeriyle istediğiniz işlemler
                    dbContext.SaveChanges();
                }
                else
                {
                    // Eslesen satır bulunamazsa
                    throw new Exception("Eşleşen satir bulunamadi!");
                }
            }

            return RedirectToAction("Index", "RecordList");

        }
    }
}
