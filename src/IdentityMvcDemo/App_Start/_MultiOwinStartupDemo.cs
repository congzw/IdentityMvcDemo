using IdentityMvcDemo;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("StartupFromA", typeof(StartupA))]
[assembly: OwinStartup("StartupFromB", typeof(StartupB))]
namespace IdentityMvcDemo
{
    public class StartupA
    {
        public void Configuration(IAppBuilder app)
        {
            //<add key="owin:appStartup" value="StartupFromA"/>
            //just for demo
        }
    }

    public class StartupB
    {
        public void Configuration(IAppBuilder app)
        {
            //<add key="owin:appStartup" value="StartupFromB"/>
            //just for demo
        }
    }
}
