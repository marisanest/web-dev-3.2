using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web_Dev3.Startup))]
namespace Web_Dev3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
