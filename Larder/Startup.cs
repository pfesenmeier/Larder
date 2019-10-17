using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Larder.Startup))]
namespace Larder
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
