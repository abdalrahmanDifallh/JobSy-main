using JopSy.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace JopSy.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        //[HttpPost]
        //public async Task<IActionResult> Register(RegisterViewModel registerView)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        User
        //    }
        //}


        public IActionResult Verify()
        {
            return View();
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
    }
}
