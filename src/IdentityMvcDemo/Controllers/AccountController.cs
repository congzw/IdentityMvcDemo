using System.Web;
using System.Web.Mvc;
using IdentityMvcDemo.Models;
using IdentityMvcDemo.Models.DemoUsers;
using IdentityMvcDemo.Models.Gotchas;
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

        #region gotcha

        public ActionResult Login2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login2(LoginViewModel2 model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var validateCode = Session["ValidateCode"] as string;
            if (string.IsNullOrWhiteSpace(validateCode) || validateCode != model.ValidateCode)
            {
                ModelState.AddModelError("", "Validate Code is incorrect.");
                return View(model);
            }

            var result = SignInManager.PasswordSignIn(model.UserName, model.Password, false, false);
            if (result != SignInStatus.Success)
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult GetValidateCode()
        {
            var vCode = new ValidateCodeHelper();
            string code = vCode.CreateValidateCode(4);
            Session["ValidateCode"] = code;
            byte[] bytes = vCode.CreateValidateGraphic(code);
            return File(bytes, @"image/jpeg");
        }

        public ActionResult GetJsonData()
        {
            return Json(Session["ValidateCode"]);
        }

        //然后在view中页面调用
        //<img id="imgCode" src="~/[Controller]/GetValidateCode" style="cursor: pointer; vertical-align: middle" alt="ValidateCode" />

        //最后单击无刷新更换验证码(使用jquery)
        //$("#imgCode").click(function() { this.src = "/[Controller]/GetValidateCode?time" + new Date().getTime(); });//后面一定new一个date不然不会进行换组

        //在页面中写点击验证码图片的事件
        //$.ajax({ url:”GetJsonData”,type:”post”,dataType:”json”,success: showResult})
        //function showResult(result) { $("#hCode").val(result.toString()); };

        #endregion

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
