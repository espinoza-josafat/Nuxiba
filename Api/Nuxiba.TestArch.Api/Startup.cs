using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Nuxiba.TestArch.Web.Startup))]
namespace Nuxiba.TestArch.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}