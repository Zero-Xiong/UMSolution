using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace UMWebApi
{
    public class MyJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer;
        private readonly string _audience;
        private readonly TimeSpan _expiration;
        private readonly byte[] _key;


        public MyJwtFormat(string issuer, string audience, byte[] key, TimeSpan expiration)
        {
            _issuer = issuer;
            _audience = audience;
            _key = key;
            _expiration = expiration;
        }

        public string SignatureAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"; }
        }

        public string DigestAlgorithm
        {
            get { return "http://www.w3.org/2001/04/xmlenc#sha256"; }
        }


        public string Protect(AuthenticationTicket data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));

            //var key = Convert.FromBase64String("UHxNtYMRYwvfpO1dS5pWLKL0M2DgOj40EbN4SoBWgfc");
            var now = DateTime.UtcNow;

            var signingCredentials = new SigningCredentials(
                                    new SymmetricSecurityKey(_key),
                                    SignatureAlgorithm,
                                    DigestAlgorithm);

            var token = new JwtSecurityToken(_issuer, _audience, data.Identity.Claims, now, now.AddMinutes(_expiration.TotalMinutes), signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}
