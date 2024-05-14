using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moqups.Areas.Admin.Models;
using Services.Contracts;

namespace Moqups.Areas.Admin.Controllers
{
    [Area("Admin")] //Area'larla calisirken hangi area altinda calisildigi bilgisi verilmezse sayfa yuklenmez.
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IServiceManager _manager;

        public UserController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var users = _manager.AuthService.GetAllUsers();
            return View(users);
        }

        public IActionResult Create()
        {
            return View(new UserDtoForCreation()
            {
                Roles = new HashSet<string>(_manager
                .AuthService
                .Roles
                .Select(r => r.Name) //rol isimleri alinir.
                .ToList())
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
        {
            var result = await _manager.AuthService.CreateUser(userDto);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View(result);
        }

        public async Task<IActionResult> Update([FromRoute(Name = "id")] string id)
        {
            var user = await _manager.AuthService.GetOneUserForUpdate(id); //id icerisinde usernName olacak.
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] UserDtoForUpdate userDto)
        {
            if (ModelState.IsValid)
            {
                await _manager.AuthService.Update(userDto);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ResetPassword([FromRoute(Name = "id")] string id)
        {
            return View(new ResetPasswordDto()
            {
                UserName = id
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            var result = await _manager.AuthService.ResetPassword(model);
            return result.Succeeded
                ? RedirectToAction("Index")
                : View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteOneUser([FromForm] UserDto userDto) //Binding islemi
        {
            var result = await _manager
                .AuthService
                .DeleteOneUser(userDto.UserName);

            return result.Succeeded //Kullanici silindiyse
                ? RedirectToAction("Index")
                : View();
        }
    }


    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
    //public class UserController : Controller
    //{
    //    private readonly UserManager<User> _userManager;

    //    public UserController(UserManager<User> userManager)
    //    {
    //        _userManager = userManager;
    //    }

    //    public IActionResult Index()
    //    {
    //        var users = _userManager.Users;
    //        return View(users);
    //    }

    //    //public IActionResult Create()
    //    //{
    //    //    return View(new UserDtoForCreation()
    //    //    {
    //    //        Roles = new HashSet<string>(_userManager
    //    //        .GetRolesAsync(_userManager.Users)
    //    //        .Result) // Kullanıcı rollerini alma
    //    //    });
    //    //}

    //    //[HttpPost]
    //    //[ValidateAntiForgeryToken]
    //    //public async Task<IActionResult> Create([FromForm] UserDtoForCreation userDto)
    //    //{
    //    //    if (ModelState.IsValid)
    //    //    {
    //    //        var user = new User { UserName = userDto.UserName, Email = userDto.Email };
    //    //        var result = await _userManager.CreateAsync(user, userDto.Password);

    //    //        if (result.Succeeded)
    //    //        {
    //    //            await _userManager.AddToRolesAsync(user, userDto.Roles);
    //    //            return RedirectToAction("Index");
    //    //        }
    //    //    }
    //    //    return View(userDto);
    //    //}
    //}
}

