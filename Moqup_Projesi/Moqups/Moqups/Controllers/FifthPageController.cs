using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moqups.Models;
using Repositories;

namespace Moqups.Controllers
{

    public class FifthPageController : Controller
    {
        private readonly ILogger<FifthPageController> _logger;
        private readonly RepositoryContext _context;
        private readonly IConfiguration _configuration;

        public FifthPageController(ILogger<FifthPageController> logger, IConfiguration configuration, RepositoryContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }
        public async Task<IActionResult> Index(string takipNo)
        {
            ViewBag.PageName = "Takip Sonucu";
            ViewBag.PageNumber = 5;
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
            GetCallbackMessageAndTakipNo model = null;
            using (var dbContext = new RepositoryContext(optionsBuilder.Options))
            {
                // GeriDonusMesaj kolonu ile eşleşen bir TakipNumaralari satırını bul
                var takipNumarasiSatiri = await dbContext.TakipNumaralari
                    .FirstOrDefaultAsync(t => t.TakipNumarası == takipNo);

                if (takipNumarasiSatiri != null)
                {
                    var query = from takipNumaralari in _context.TakipNumaralari
                                join callbackMessages in _context.CallbackMessages on takipNumaralari.Id equals callbackMessages.TakipNoId
                                where takipNumaralari.Id == takipNumarasiSatiri.Id
                                select new GetCallbackMessageAndTakipNo
                                {
                                    takipNoId = takipNumaralari.Id,
                                    takipNoString = takipNumaralari.TakipNumarası,
                                    geriDonusMesaji = callbackMessages.geriDonusMesaj,
                                    geriDonusId = callbackMessages.Id,
                                };
                    var result = query.SingleOrDefault();
                    return View(result);
                }
                else
                {
                    // Eşleşen satır bulunamadı
                }
            }
            return View(model);
        }
    }
}
