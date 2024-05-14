using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moqups.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Repositories;
using Entities.Models;

namespace Moqups.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CallBackGosterController : Controller
    {
        private readonly ILogger<CallBackGosterController> _logger;
        private readonly RepositoryContext _context;
        //private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public CallBackGosterController(ILogger<CallBackGosterController> logger, IConfiguration configuration, RepositoryContext context)
        {
            _logger = logger;
            _configuration = configuration;
            _context = context;
        }

        public async Task<IActionResult> Index(string takipNo)
        {
            ViewBag.PageName = 8;
            ViewBag.PageTitle = "CallBack Message Goster";

            // DbContextOptionsBuilder kullanımını kaldırabilirsiniz, çünkü DbContext doğrudan injection yapıldı.

            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var dbContext = new RepositoryContext(optionsBuilder.Options))
            {
                // GeriDonusMesaj kolonu ile eşleşen bir TakipNumaralari satırı bulundu.
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
                    // Eşleşen satır bulunamazsa
                    throw new Exception("Eşleşen satir bulunamadi!");
                }
            }

            // Yalnızca _context'i kullanabilirsiniz, doğrudan injection yaptınız.

            // RepositoryContext'ten ApplicationDbContext elde et
            //var dbContext = _context.GetDbContext();

            //var takipNumarasiSatiri = await dbContext.TakipNumaralari.FirstOrDefaultAsync(t => t.TakipNumarası == takipNo);

            //var message = _context.CallbackMessages
            //    .FirstOrDefault(m => m.TakipNoId == takipNo);

            //if (takipNumarasiSatiri != null)
            //{
            //    var query = from takipNumaralari in dbContext.TakipNumaralari
            //                join callbackMessages in dbContext.CallbackMessages on takipNumaralari.Id equals callbackMessages.TakipNoId
            //                where takipNumaralari.Id == takipNumarasiSatiri.Id
            //                select new GetCallbackMessageAndTakipNo
            //                {
            //                    takipNoId = takipNumaralari.Id,
            //                    takipNoString = takipNumaralari.TakipNumarası,
            //                    geriDonusMesaji = callbackMessages.geriDonusMesaj,
            //                    geriDonusId = callbackMessages.Id,
            //                };
            //    var result = await query.SingleOrDefaultAsync(); // Asenkron olmalı
            //    return View(result);
            //}
            //else
            //{
            //    // Eşleşen satır bulunamazsa
            //    throw new Exception("Eşleşen satir bulunamadi!");
            //}
        }

    }
}



