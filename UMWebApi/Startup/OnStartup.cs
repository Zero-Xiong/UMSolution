using AutoMapper;
using Microsoft.Owin.Logging;
using Owin;

namespace UMWebApi
{
    public partial class Startup
    {
        Startup ConfigStartup(IAppBuilder app)
        {
            var log = app.CreateLogger<Startup>();
            log.WriteInformation("Webapi is running.");

            InitAutoMapper();


            log.WriteInformation("Webapi is initialized.");

            return this;
        }

        private void InitAutoMapper()
        {
            Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<>
            });
        }
    }
}