using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using GerenciadorDeTarefas.Models;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorDeTarefas.Services
{
    public class TokenService
    {
        public static string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyInBytes = Encoding.ASCII.GetBytes(JwtKey.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Sid, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyInBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}