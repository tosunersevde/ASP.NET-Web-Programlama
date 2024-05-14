using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Moqups.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IServiceManager _manager;

        public RoleController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            return View(_manager.AuthService.Roles);
        }
    }

    //[Area("Admin")]
    //[Authorize(Roles = "Admin")]
    //public class RoleController : Controller
    //{
    //    private readonly RoleManager<IdentityRole> _roleManager;

    //    public RoleController(RoleManager<IdentityRole> roleManager)
    //    {
    //        _roleManager = roleManager;
    //    }

    //    public IActionResult Index()
    //    {
    //        var roles = _roleManager.Roles;
    //        return View(roles);
    //    }
    //}
}
