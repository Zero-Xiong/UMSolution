using Owin;
using System;
using System.Configuration;
using System.Text;

namespace UMWebApi
{
    public partial class Startup
    {
        Startup ConfigureAuth(IAppBuilder app)
        {
            string issuer = ConfigurationManager.AppSettings["jwt:issuer"];
            string audience = ConfigurationManager.AppSettings["jwt:audience"];
            string secret = ConfigurationManager.AppSettings["jwt:secret"];
            string tokenpath = ConfigurationManager.AppSettings["jwt:tokenpath"];

            TimeSpan expiration = TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["jwt:expiration"]));

            byte[] key = Encoding.UTF8.GetBytes(secret);

            app.UseOAuthAuthorizationServer(new MyOAuthAuthorizationServerOptions(tokenpath, expiration, issuer, audience, key));

            app.UseJwtBearerAuthentication(new MyJwtBearerAuthenticationOptions(issuer, audience, key));

            return this;
        }
    }
}