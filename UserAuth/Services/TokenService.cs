
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserAuth.Model;

namespace UserAuth.Services
{
    public class TokenService
    {
        public string GenerateToken(User user)
        {
            Claim[] claims = new Claim[]
            {
                   new Claim("userName", user.UserName),
                   new Claim("registrationNumber", user.RegistrationNumber),
                   new Claim("dateOfBirth", user.DateOfBirth.ToString()),
            };

            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ljkbbHJKL345632RSDFDFBFGDDfsdfdjy"));
            var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredentials
             );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}