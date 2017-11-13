using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebDevProject.Controllers
{
    [Authorize]
    public class MemberHomeController : Controller
    {
        // GET: /<controller>/
        [Route("MemberHome")]
        public IActionResult Index()
        {
                return View();
        }

        public IActionResult AccessGranted()
        {
                return View();
        }

        [AllowAnonymous]
        public IActionResult AnonymousAccess()
        {
            return View();
        }
    }
}
