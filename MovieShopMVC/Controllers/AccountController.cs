using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var userSuccess = await _accountService.ValidateUser(model);
            if ( userSuccess!=null && userSuccess.Id >0)
            {
                // pasword matches
                // redirect to home page
                return LocalRedirect("~/");
            }
            return View();
        }

        public IActionResult Register()
        {
            // showing empty register view
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            var userId = await _accountService.RegisterUser(model);

            if (userId>0)
            {
                // redirect to login page
                return RedirectToAction("Login");
            }

            return View();

        }
    }
}
