//using Microsoft.Owin;
using Owin;

//[assembly: OwinStartupAttribute(typeof(DevNet.Startup))]
namespace DevNet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
