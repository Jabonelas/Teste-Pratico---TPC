using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Teste_Pratico_API_TPC.Data;
using Teste_Pratico_API_TPC.Interfaces;

namespace Teste_Pratico_API_TPC.Services
{
    public class AutenticacaoService : IAutenticacao
    {
        private readonly BancoTPCContext context;

        private readonly IConfiguration configuration;

        public AutenticacaoService(BancoTPCContext _context, IConfiguration _configuration)
        {
            context = _context;
            configuration = _configuration;
        }

        public string GenerateToken(string _email, string _senha)
        {
            var claims = new[]
            {
                new Claim("email", _email),
                new Claim("senha", _senha),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var chavePrivada = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"]));

            var credenciais = new SigningCredentials(chavePrivada, SecurityAlgorithms.HmacSha256);

            var expiracao = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(

                issuer: configuration["jwt:issuer"],
                audience: configuration["jwt:audience"],
                claims: claims,
                expires: expiracao,
                signingCredentials: credenciais

            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}