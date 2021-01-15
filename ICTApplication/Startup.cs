using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ICTApplication.Startup))]
namespace ICTApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
