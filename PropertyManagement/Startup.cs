using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PropertyManagement.Startup))]
namespace PropertyManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
