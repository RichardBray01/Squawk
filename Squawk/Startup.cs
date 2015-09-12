using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Squawk.Startup))]
namespace Squawk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
