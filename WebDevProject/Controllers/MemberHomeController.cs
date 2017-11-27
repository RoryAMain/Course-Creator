using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using WebDevProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProject.Controllers
{
    //[Authorize(Roles = "Member, Admin, Instructor")]
    public class MemberHomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberHomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: /<controller>/
        [Route("MemberHome")]
        public IActionResult Index()
        {
            var claims = User.Claims;

                return View();
        }

        public IActionResult AccessGranted()
        {
                return View();
        }

        [AllowAnonymous]
        public IActionResult AnonymousAccess()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
