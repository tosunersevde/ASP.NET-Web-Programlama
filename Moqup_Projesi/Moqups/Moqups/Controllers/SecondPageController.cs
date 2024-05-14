using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moqups.Models;
using Repositories;

namespace Moqups.Controllers
{
    public class SecondPageController : Controller
    {
        private readonly ILogger<SecondPageController> _logger;
        private readonly IConfiguration _configuration;

        public SecondPageController(ILogger<SecondPageController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewBag.PageName = "Bildirim";
            ViewBag.PageNumber = 2;
            return View();
        }
        public IActionResult AddMessage(Message model)
        {
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var (takipNoString, takipNoId) = GenerateRandomTrackingNumber();
            using (var dbContext = new RepositoryContext(optionsBuilder.Options))
            {
                model.isOk = false;
                model.TakipNoId = takipNoId;
                //gelen modeli ekle
                dbContext.Messages.Add(model);

                // Değişiklikleri kaydedin
                dbContext.SaveChanges();
                TempData["takipNo"] = takipNoString;//bu şekilde url de göndermiyorum ama yine TempData ile controllerda karşılıyacağım
            }
            return RedirectToAction("Index","ThirdPage", new {takipNo = takipNoString});
        }
        private (string TakipNumarasi, int TakipNoId) GenerateRandomTrackingNumber()
        {
            var connectionString = _configuration.GetConnectionString("SqlServerConnection");
            var optionsBuilder = new DbContextOptionsBuilder<RepositoryContext>();
            optionsBuilder.UseSqlServer(connectionString);
            // Rastgele sayı ve harf içeren bir dize oluştur
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            Random random = new Random();
            char[] trackingNumberArray = new char[20];
            string yeniTakipNoString = "";
            int yeniTakipNoId = 0;
            for (int i = 0; i < 20; i++)
            {
                trackingNumberArray[i] = characters[random.Next(characters.Length)];
            }
            try
            {
                var yeniTakipNo = new TakipNo
                {
                    TakipNumarası = new string(trackingNumberArray),
                };
                using (var dbContext = new RepositoryContext(optionsBuilder.Options))
                {
                    
                    //gelen modeli ekle
                    dbContext.TakipNumaralari.Add(yeniTakipNo);

                    // Değişiklikleri kaydedin
                    dbContext.SaveChanges();
                    yeniTakipNoString = yeniTakipNo.TakipNumarası;
                    yeniTakipNoId = yeniTakipNo.Id;
                }
            }
            catch (Exception ex)
            {
                (yeniTakipNoString, yeniTakipNoId) = GenerateRandomTrackingNumber();
            }
            // Char dizisini string'e dönüştür
            return (yeniTakipNoString, yeniTakipNoId);
        }
    }
}
