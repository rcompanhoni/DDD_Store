using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LuaBijoux.Bootstrapper.Startup))]
namespace LuaBijoux.Bootstrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
