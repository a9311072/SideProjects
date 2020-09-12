using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ASPNET_MVC_5.Startup))]
namespace ASPNET_MVC_5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
