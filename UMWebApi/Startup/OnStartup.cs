using AutoMapper;
using Owin;

namespace UMWebApi
{
    public partial class Startup
    {
        Startup ConfigStartup(IAppBuilder app)
        {

            InitAutoMapper();
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