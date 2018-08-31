using System;
using System.Collections.Generic;

namespace IdentityMvcDemo.Models.DemoUsers
{
    public interface IDemoUserRepository
    {
        IEnumerable<DemoUser> Query();
        void SaveOrUpdate(DemoUser demoUser);
        DemoUser Get(Guid userId);
    }
}