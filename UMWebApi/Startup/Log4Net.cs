using log4net.Core;
using MB.Owin.Logging.Log4Net;
using Microsoft.Owin.Logging;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UMWebApi
{
    public partial class Startup
    {
        Startup ConfigLog4Net(IAppBuilder app)
        {
            var config = "~/log4net.config";
            app.UseLog4Net(config);

            var logger = app.CreateLogger<Startup>();
            return this;
        }
    }
}