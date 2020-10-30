using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GymManager.Startup))]
namespace GymManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
