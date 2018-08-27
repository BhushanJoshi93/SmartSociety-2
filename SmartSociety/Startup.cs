using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SmartSociety.Startup))]
namespace SmartSociety
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
