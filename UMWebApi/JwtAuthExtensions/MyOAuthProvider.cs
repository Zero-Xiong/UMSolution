using Autofac;
using Microsoft.Owin.Security.OAuth;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using UMServices;

namespace UMWebApi
{
    public class MyOAuthProvider : OAuthAuthorizationServerProvider
    {
        //private readonly IComponentContext _context;
        //public CustomOAuthProvider(IComponentContext context)
        //{
        //    _context = context;
        //}

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientid, clientsecret;
            var username = context.Parameters["username"];
            var password = context.Parameters["password"];

            if (!context.TryGetBasicCredentials(out clientid, out clientsecret) ||
                context.TryGetFormCredentials(out clientid, out clientsecret))
            {
                if (string.IsNullOrWhiteSpace(clientid) && string.IsNullOrWhiteSpace(clientsecret))
                {
                    context.Validated();
                    return Task.FromResult(0);
                }

                if (clientid == ConfigurationManager.AppSettings["clientid"] &&
                    clientsecret == ConfigurationManager.AppSettings["clientsecret"])
                {
                    context.Validated(clientid);
                }
                else
                {
                    context.SetError("invalid_clientid", "The valid clientid could not be retrieved from Authentication Header");
                    context.Rejected();
                }
            }
            else
            {
                context.SetError("invalid_clientid", "The valid clientid could not be retrieved from Authentication Header");
                context.Rejected();
            }

            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var password = context.Password;
            var username = context.UserName;

            var identity = new ClaimsIdentity("otc");

            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", username));
            identity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "user"));

            var scope = Autofac.Integration.Owin.OwinContextExtensions.GetAutofacLifetimeScope(context.OwinContext);
            var service = scope.Resolve<IUserService>();

            var user = service.FindUser(username, password);

            if (user != null)
                context.Validated(identity);
            else
            {
                context.SetError("invalid_grant", "username or password is wrong.");
                context.Rejected();
            }

            return Task.FromResult(0);
        }

        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {

            return base.GrantClientCredentials(context);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            return base.GrantRefreshToken(context);
        }

        public override Task AuthorizationEndpointResponse(OAuthAuthorizationEndpointResponseContext context)
        {
            return base.AuthorizationEndpointResponse(context);
        }

        public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            return base.AuthorizeEndpoint(context);
        }

        public override Task GrantAuthorizationCode(OAuthGrantAuthorizationCodeContext context)
        {
            return base.GrantAuthorizationCode(context);
        }

        public override Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
            return base.GrantCustomExtension(context);
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            if (context.IsTokenEndpoint && context.Request.Method == "OPTIONS")
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Headers", new[] { "authorization" });
                context.RequestCompleted();
                return Task.FromResult(0);
            }

            return base.MatchEndpoint(context);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            return base.TokenEndpoint(context);
        }

        public override Task TokenEndpointResponse(OAuthTokenEndpointResponseContext context)
        {
            return base.TokenEndpointResponse(context);
        }

        public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return base.ValidateAuthorizeRequest(context);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return base.ValidateClientRedirectUri(context);
        }

        public override Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        {
            return base.ValidateTokenRequest(context);
        }
    }
}