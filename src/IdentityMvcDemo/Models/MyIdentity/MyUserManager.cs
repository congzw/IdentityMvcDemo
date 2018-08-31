using System;
using IdentityMvcDemo.Models.DemoUsers;
using Microsoft.AspNet.Identity;

namespace IdentityMvcDemo.Models.MyIdentity
{
    public class MyUserManager : UserManager<DemoUser, Guid>
    {
        public MyUserManager(IUserStore<DemoUser, Guid> store) : base(store)
        {
            UserValidator = new UserValidator<DemoUser, Guid>(this);
            PasswordValidator = new PasswordValidator() { RequiredLength = 6 };
            PasswordHasher = new PasswordHasher();
        }
    }
}