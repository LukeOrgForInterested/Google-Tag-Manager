using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Google_Tag_Manager.Startup))]
namespace Google_Tag_Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
