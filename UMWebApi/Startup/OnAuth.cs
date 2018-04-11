using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
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
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions()
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["jwt:expiration"])),
                AccessTokenFormat = new CustomJwtFormat(
                    ConfigurationManager.AppSettings["jwt:issuer"],
                    ConfigurationManager.AppSettings["jwt:audience"],
                    Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["jwt:secret"]),
                    TimeSpan.FromMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["jwt:expiration"]))),
#if DEBUG
                AllowInsecureHttp = true,
#endif
                TokenEndpointPath = new PathString("/api/account/token"),
                Provider = new CustomOAuthProvider(),
            });


            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions()
            {
                TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = ConfigurationManager.AppSettings["jwt:issuer"], // site that makes the token
                    ValidateIssuer = true,
                    ValidAudience = ConfigurationManager.AppSettings["jwt:audience"], // site that consumes the token
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["jwt:secret"])),
                    ValidateIssuerSigningKey = true, // verify signature to avoid tampering
                    ValidateLifetime = true, // validate the expiration
                    ClockSkew = TimeSpan.Zero // tolerance for the expiration date
                },
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new string[] { ConfigurationManager.AppSettings["jwt:audience"] },
                IssuerSecurityKeyProviders = new IIssuerSecurityKeyProvider[] {
                    new SymmetricKeyIssuerSecurityKeyProvider(
                        ConfigurationManager.AppSettings["jwt:issuer"],
                        Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["jwt:secret"]))
                },

            });

            return this;
        }
    }
}