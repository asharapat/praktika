using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(taskTwo.Startup))]
namespace taskTwo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
