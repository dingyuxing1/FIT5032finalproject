using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fit5032Assignment.Startup))]
namespace Fit5032Assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
