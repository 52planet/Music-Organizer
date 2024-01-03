using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(F2021A5LB.Startup))]

namespace F2021A5LB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
