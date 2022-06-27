using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevasolVidly.Startup))]
namespace DevasolVidly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
