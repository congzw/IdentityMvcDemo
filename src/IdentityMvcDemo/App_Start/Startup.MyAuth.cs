using IdentityMvcDemo.Models.DemoUsers;
using IdentityMvcDemo.Models.MyIdentity;
using IdentityMvcDemo.Models.MySignIn;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace IdentityMvcDemo
{
    public partial class Startup
    {
        public void ConfigureMyAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new MyUserManager(new MyIdentityStore(new DemoUserRepository())));
            app.CreatePerOwinContext<MySignInManager>((options, context) => new MySignInManager(context.GetUserManager<MyUserManager>(), context.Authentication));

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}