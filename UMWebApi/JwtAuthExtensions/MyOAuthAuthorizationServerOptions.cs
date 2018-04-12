using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;

namespace UMWebApi
{
    public class MyOAuthAuthorizationServerOptions : OAuthAuthorizationServerOptions
    {
        public MyOAuthAuthorizationServerOptions(
            string tokenpath,
            TimeSpan expiration,
            string issuer,
            string audience,
            byte[] key
            )
        {
            TokenEndpointPath = new PathString(tokenpath);
            AccessTokenExpireTimeSpan = expiration;

            AccessTokenFormat = new MyJwtFormat(issuer, audience, key, expiration);

            Provider = new MyOAuthProvider();
#if DEBUG
            AllowInsecureHttp = true;
#endif
        }
    }
}
