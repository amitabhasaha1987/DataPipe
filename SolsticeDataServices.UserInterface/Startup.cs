using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SolsticeDataServices.UserInterface.Startup))]
namespace SolsticeDataServices.UserInterface
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
