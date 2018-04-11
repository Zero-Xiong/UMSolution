using Autofac;
using UMDataAccess;
using UMServices;

namespace UMWebApi
{
    public partial class Startup
    {
        private void RegisterTypeResolver(ContainerBuilder builder)
        {
            //Repository
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().SingleInstance();
            builder.RegisterType<UserRepository>().As<IUserRepository>().SingleInstance();

            //Services
            builder.RegisterType<SecurityService>().As<ISecurityService>().SingleInstance();
            builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
        }
    }
}