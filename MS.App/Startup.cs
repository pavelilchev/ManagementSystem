using Microsoft.Owin;
using MS.App;

[assembly: OwinStartup(typeof(Startup))]
namespace MS.App
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            this.ConfigureAuth(app);
        }
    }
}
