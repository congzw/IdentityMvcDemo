using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityMvcDemo.Models.DemoUsers
{
    public class DemoUserRepository : IDemoUserRepository
    {
        public static IList<DemoUser> Users = new List<DemoUser>();

        static DemoUserRepository()
        {
            var passwordHasher = PasswordHasherHelper.PasswordHasher();
            Users.Add(new DemoUser() { Id = Guid.NewGuid(), UserName = "admin", PasswordHash = passwordHasher.HashPassword("111") });
            Users.Add(new DemoUser() { Id = Guid.NewGuid(), UserName = "John", PasswordHash = passwordHasher.HashPassword("111") });
            Users.Add(new DemoUser() { Id = Guid.NewGuid(), UserName = "Scott", PasswordHash = passwordHasher.HashPassword("111") });
        }

        public IEnumerable<DemoUser> Query()
        {
            return Users;
        }

        public void SaveOrUpdate(DemoUser demoUser)
        {
            var theOne = Users.FirstOrDefault(x => x.Id == demoUser.Id);
            if (theOne == null)
            {
                Users.Add(demoUser);
            }
            //todo update
        }

        public DemoUser Get(Guid userId)
        {
            var theOne = Users.FirstOrDefault(x => x.Id == userId);
            return theOne;
        }
    }
}