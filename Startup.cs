using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCLearning.Startup))]
namespace MVCLearning
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
