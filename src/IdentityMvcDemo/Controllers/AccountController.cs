using System.Web;
using System.Web.Mvc;
using IdentityMvcDemo.Models;
using IdentityMvcDemo.Models.DemoUsers;
using IdentityMvcDemo.Models.IoC;
using IdentityMvcDemo.Models.MyIdentity;
using IdentityMvcDemo.Models.MySignIn;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace IdentityMvcDemo.Controllers
{
    public class AccountController : Controller
    {
        private MySignInManager _signInManager;
        private MyUserManager _userManager;
        public MySignInManager SignInManager
        {
            get
            {
                return _signInManager ?? MyServiceLocator.GetSignInManager();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public MyUserManager UserManager
        {
            get { return _userManager ?? MyServiceLocator.GetUserManager(); }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = SignInManager.PasswordSignIn(model.UserName, model.Password, false, false);
                if (result == SignInStatus.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new DemoUser() { UserName = model.UserName };
                var result = UserManager.Create(user, model.Password);
                if (result.Succeeded)
                {
                    SignInManager.SignIn(user, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}
