using System;
using System.IdentityModel.Tokens.Jwt;

namespace Authentication
{
    public class JwtToken
    {
        private JwtSecurityToken token;
        private string stringToken;
        public JwtToken(JwtSecurityToken token)
        {
            this.token = token;
        }

        public JwtToken(string token)
        {
            this.stringToken = token;
        }

        public DateTime ValidTo => token.ValidTo;

        public string Value => new JwtSecurityTokenHandler().WriteToken(this.token);

        public JwtSecurityToken Read => new JwtSecurityTokenHandler().ReadJwtToken(this.stringToken);
    }
}