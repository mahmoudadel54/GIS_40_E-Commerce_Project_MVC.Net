using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Our_Ecommerce_store_GIS_40.Startup))]
namespace Our_Ecommerce_store_GIS_40
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
