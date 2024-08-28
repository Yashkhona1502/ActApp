using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ACTApp.Interfaces;
using ACTApp.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ACTApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserIdentity userIdentity;

        public AccountController(IUserIdentity userIdentity)
        {
            this.userIdentity = userIdentity;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var CheckUser = userIdentity.CheckUserLogin(loginViewModel.UserName, loginViewModel.Password);

            if (CheckUser != null && CheckUser.Status == "Approved")
            {
                return View("Views/Home/Index.cshtml");
            }
            else
            {
                if(CheckUser != null && CheckUser.Status == "Pending")
                {
                    ModelState.AddModelError("UserName", "Account is not approved");
                }
                else
                {
                    ModelState.AddModelError("UserName", "Invalid Credentials");
                }
                return View(new LoginViewModel());
            }
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = registerViewModel.UserName,
                    EmailId = registerViewModel.EmailId,
                    Mobile = registerViewModel.Mobile,
                    Password = registerViewModel.Password,
                    Status = "Pending"
                };
                var email = userIdentity.FindByEmail(registerViewModel.EmailId);

                var mobile = userIdentity.FindByMobile(registerViewModel.Mobile);
                if(email != null)
                {
                    ModelState.AddModelError("EmailId", "Email already exist");
                    return View(new RegisterViewModel());
                }
                if(mobile != null)
                {
                    ModelState.AddModelError("Mobile","Mobile already exisit");
                    return View(new RegisterViewModel());
                }
                var updateuser = userIdentity.AddUser(appUser);
            }
            return View("Views/Account/Login.cshtml");
        }
    }
}

