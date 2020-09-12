using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Carts.Startup))]
namespace MVC_Carts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
