using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Travel_App.Startup))]
namespace Travel_App
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
