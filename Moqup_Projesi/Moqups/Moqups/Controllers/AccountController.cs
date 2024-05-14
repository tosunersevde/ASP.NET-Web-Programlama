using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moqups.Models;

namespace Moqups.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager; //injection islemi
        private readonly SignInManager<IdentityUser> _signInManager; //oturum acmis kullanicilar icin oturum sonlandirma vs.

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public IActionResult Login()
        public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
        {
            //return View();
            return View(new LoginModel()
            {
                ReturnUrl = ReturnUrl
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (ModelState.IsValid) //Form gecerliyse
            {
                IdentityUser user = await _userManager.FindByNameAsync(model.UserName);
                if (user is not null) //Kullanici tanimliysa
                {
                    await _signInManager.SignOutAsync(); //Kullanici varsa logout edilir.
                    //Oturum acma
                    if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
                    {
                        return Redirect(model?.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("Error", "Invalid username or password!");
            }
            return View();
        }

        public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/") //hangi sayfadan logout oldu
        {
            await _signInManager.SignOutAsync();
            return Redirect(ReturnUrl); //Cikis yapilan sayfaya yoksa anasayfaya gidecek.
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromForm] RegisterDto model)
        {
            //Kullanici olustur
            var user = new IdentityUser //Passwordler'in sifrelenmesi, veritabaninda acik olarak tutulmamasi icin burda verilmez.
            {
                UserName = model.UserName,
                Email = model.Email,
            };

            //Kullaniciyi kaydet
            var result = await _userManager
                .CreateAsync(user, model.Password);

            //Rol bilgisi ekle, kullanici basarili bir sekilde eklenirse
            if (result.Succeeded)
            {
                var roleResult = await _userManager //web'den gelen kullanici rol yetkisine sahip olacak.
                    .AddToRoleAsync(user, "User");

                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Login", new { ReturnUrl = "/" }); //Anonim nesne tanimi
                }
            }
            else
            {
                foreach (var err in result.Errors) //Identity result ile sonuclar burda saklanir.
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View();
        }

        public IActionResult AccessDenied([FromQuery(Name = "ReturnUrl")] string returnUrl)
        {
            return View();
        }
    }
}


//public class AccountController : Controller
//{

//    private readonly UserManager<IdentityUser> _userManager; //injection islemi
//    private readonly SignInManager<IdentityUser> _signInManager; //oturum acmis kullanicilar icin oturum sonlandirma vs.

//    public AccountController(UserManager<IdentityUser> userManager,
//        SignInManager<IdentityUser> signInManager)
//    {
//        _userManager = userManager;
//        _signInManager = signInManager;
//    }

//    public IActionResult Index()
//    {
//        return View();
//    }

//    //public IActionResult Login([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
//    public IActionResult Login()
//    {
//        return View();
//        //return View(new LoginModel()
//        //{
//        //    ReturnUrl = ReturnUrl
//        //});
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]

//    public IActionResult Login([FromForm] LoginModel model)
//    {
//        if (ModelState.IsValid) // Form geçerliyse
//        {
//            // Kullanıcı kimlik doğrulamasını yapın
//            // Başarılıysa yönlendirme yapın
//            return RedirectToAction("Index", "Home");
//        }
//        // Geçerli değilse aynı view'i tekrar göster
//        return View(model);
//    }

//    //public async Task<IActionResult> Logout([FromQuery(Name = "ReturnUrl")] string ReturnUrl = "/")
//    public async Task<IActionResult> Logout()
//    {
//        await HttpContext.SignOutAsync(); // Oturumu sonlandır
//        return RedirectToAction("Index", "Home"); // Ana sayfaya yönlendir
//    }

//    //[HttpPost]
//    //[ValidateAntiForgeryToken]
//    //public async Task<IActionResult> Login([FromForm] LoginModel model)
//    //{
//    //    if (ModelState.IsValid) //Form gecerliyse
//    //    {
//    //        IdentityUser user = await _userManager.FindByNameAsync(model.Email);
//    //        if (user is not null) //Kullanici tanimliysa
//    //        {
//    //            await _signInManager.SignOutAsync(); //Kullanici varsa logout edilir.
//    //            //Oturum acma
//    //            if ((await _signInManager.PasswordSignInAsync(user, model.Password, false, false)).Succeeded)
//    //            {
//    //                return Redirect(model?.ReturnUrl ?? "/");
//    //            }
//    //        }
//    //        ModelState.AddModelError("Error", "Invalid username or password!");
//    //    }
//    //    return View();
//    //}

//    public IActionResult Register()
//    {
//        return View();
//    }

//    [HttpPost]
//    [ValidateAntiForgeryToken]
//    public async Task<IActionResult> Register([FromForm] Register model)
//    {
//        //Kullanici olustur
//        var user = new IdentityUser //Passwordler'in sifrelenmesi, veritabaninda acik olarak tutulmamasi icin burda verilmez.
//        {
//            UserName = model.UserName,
//            Email = model.Email,
//        };

//        //Kullaniciyi kaydet
//        var result = await _userManager
//            .CreateAsync(user, model.Password);

//        //Rol bilgisi ekle, kullanici basarili bir sekilde eklenirse
//        if (result.Succeeded)
//        {
//            var roleResult = await _userManager //web'den gelen kullanici rol yetkisine sahip olacak.
//                .AddToRoleAsync(user, "User");

//            if (roleResult.Succeeded)
//            {
//                return RedirectToAction("Login", new { ReturnUrl = "/" }); //Anonim nesne tanimi
//            }
//        }
//        else
//        {
//            foreach (var err in result.Errors) //Identity result ile sonuclar burda saklanir.
//            {
//                ModelState.AddModelError("", err.Description);
//            }
//        }

//        //return View();
//        return View(model);
//    }

//    public IActionResult AccessDenied()
//    {
//        return View();
//    }
//}