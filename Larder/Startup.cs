using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Larder.WebMVC.Startup))]
namespace Larder.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
