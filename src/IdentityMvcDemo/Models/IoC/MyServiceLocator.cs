using System.Web;
using IdentityMvcDemo.Models.MyIdentity;
using IdentityMvcDemo.Models.MySignIn;
using Microsoft.AspNet.Identity.Owin;

namespace IdentityMvcDemo.Models.IoC
{
    //should pick a real di container to solve depencency injection!
    public class MyServiceLocator
    {
        public static MyUserManager GetUserManager()
        {
            return HttpContext.Current.GetOwinContext().Get<MyUserManager>();
        }

        public static MySignInManager GetSignInManager()
        {
            return HttpContext.Current.GetOwinContext().Get<MySignInManager>();
        }
    }
}
