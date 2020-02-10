using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace Authentication
{
    public class JwtManager
    {
        public JwtTokenBuilder TokenBuilder { get; set; }
        private IConfiguration config;
        public JwtToken Token { get; set; }
        public string ValidTo { get; set; }

        public JwtManager(IConfiguration _config, string userEmail, string userId)
        {
            config = _config;
            var expiryMinutes = int.Parse(config["JWT:Expiration"]) * 60 * 24 * 7;
            TokenBuilder = new JwtTokenBuilder()
                .AddSecurityKey(JwtSecurityKey.Create(config["JWT:SecretKey"]))
                .Email(userEmail)
                .Issuer(config["JWT:Issuer"])
                .Audience(config["JWT:Issuer"])
                .Claim(ClaimTypes.PrimarySid, userId)
                .Expires(expiryMinutes);
        }

        public void Claim(string type, string value)
        {
            this.TokenBuilder.Claim(type, value);
        }

        public void Build()
        {
            this.Token = this.TokenBuilder.Build();
        }
    }
}
