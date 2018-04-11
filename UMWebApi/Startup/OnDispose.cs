using Microsoft.Owin.BuilderProperties;
using Microsoft.Owin.Logging;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;

namespace UMWebApi
{
    public partial class Startup
    {
        Startup ConfigDispose(IAppBuilder app)
        {
            var properties = new AppProperties(app.Properties);
            var cancelToken = properties.OnAppDisposing;

            if (CancellationToken.None != cancelToken)
                cancelToken.Register(() => OnAppDispose(app, cancelToken));

            return this;
        }

        private void OnAppDispose(IAppBuilder app, CancellationToken cancelToken)
        {
            var log = app.CreateLogger("OnAppDispose");

            HttpRuntime runtime = (HttpRuntime)typeof(HttpRuntime).InvokeMember("_theRuntime",
                BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.GetField,
                null, null, null);

            if (runtime != null)
            {
                var shutDownMsg = runtime.GetType().InvokeMember("_shutDownMessage",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                    null, runtime, null);

                if(shutDownMsg != null)
                    log.WriteInformation($"ShutDown Message: {shutDownMsg}");

                var shutdownStack = runtime.GetType().InvokeMember("_shutDownStack",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField,
                    null, runtime, null);

                if (shutdownStack != null)
                    log.WriteInformation($"ShutDown Stack: {shutdownStack}");
            }

            log.WriteInformation("Webapi is Disposed");
        }
    }
}