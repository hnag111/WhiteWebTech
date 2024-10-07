using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WhiteWebTech.Auth.Models;
using WhiteWebTech.Auth.Service.IService;

namespace WhiteWebTech.Auth.Service
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
        {
            JwtOptions = jwtOptions.Value;
        }
        public JwtOptions JwtOptions { get; }

        public string GenerateToken(ApplicationUsers applicationUsers)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(JwtOptions.Secret);

            var claimslist = new List<Claim>
            {
                 new Claim(JwtRegisteredClaimNames.Email, applicationUsers.Email),
                 new Claim(JwtRegisteredClaimNames.Sub, applicationUsers.Id),
                 new Claim(JwtRegisteredClaimNames.Name, applicationUsers.UserName.ToString()),
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = JwtOptions.Audience,
                Issuer = JwtOptions.Issues,
                Subject = new ClaimsIdentity(claimslist),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
