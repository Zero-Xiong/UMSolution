using Microsoft.Owin.Security.Jwt;

namespace UMWebApi
{
    public class MyJwtBearerAuthenticationOptions : JwtBearerAuthenticationOptions
    {
        public MyJwtBearerAuthenticationOptions(string issuer, string audience, byte[] key)
        {
            AllowedAudiences = new[] { audience };

            IssuerSecurityKeyProviders = new[]
            {
                new SymmetricKeyIssuerSecurityKeyProvider(issuer, key)
            };
        }
    }
}
