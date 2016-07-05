using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebHookService.Startup))]
namespace WebHookService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
