using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartup(typeof(UMWebApi.Startup))]

namespace UMWebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888

            this.ConfigLog4Net(app)
                .ConfiguWebApi(app)
                .ConfigStartup(app)
                .ConfigDispose(app);

            app.Run(content =>
            {
                return content.Response.WriteAsync($"The services is running by [{DateTime.Now}]");
            });
        }
    }
}
