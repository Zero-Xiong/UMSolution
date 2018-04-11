using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin.Cors;
using Newtonsoft.Json.Serialization;
using Owin;
using System.Reflection;
using System.Web.Http;

namespace UMWebApi
{
    public partial class Startup
    {
        Startup ConfiguWebApi(IAppBuilder app)
        {
            var config = new HttpConfiguration(); //webapi config

            var builder = new ContainerBuilder(); //autofac builder

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();

            RegisterTypeResolver(builder);

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);

            //Token Based, ust JWT Bearer Token Authentication
            ConfigureAuth(app);


            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;

            app.UseCors(CorsOptions.AllowAll);
            app.UseWebApi(config);

            return this;
        }
    }
}