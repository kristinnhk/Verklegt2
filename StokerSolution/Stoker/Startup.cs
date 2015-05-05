using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stoker.Startup))]
namespace Stoker
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
