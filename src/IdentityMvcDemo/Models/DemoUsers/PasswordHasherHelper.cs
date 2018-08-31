using Antlr.Runtime.Misc;
using Microsoft.AspNet.Identity;

namespace IdentityMvcDemo.Models.DemoUsers
{
    internal static class PasswordHasherHelper
    {
        public static Func<IPasswordHasher> PasswordHasher = () => new PasswordHasher();
    }
}