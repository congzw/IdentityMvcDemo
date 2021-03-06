﻿using System;
using IdentityMvcDemo.Models.DemoUsers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace IdentityMvcDemo.Models.MySignIn
{
    public class MySignInManager : SignInManager<DemoUser, Guid>
    {
        public MySignInManager(UserManager<DemoUser, Guid> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) { }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }
    }
}
