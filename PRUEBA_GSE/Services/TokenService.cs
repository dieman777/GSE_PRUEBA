using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PRUEBA_GSE.Services
{
    public class TokenService
    {
        private readonly string _llave;
        private readonly string _credencial;


        public TokenService(IConfiguration configuracion)
        {
            _llave = configuracion["Jwt:Key"];
            _credencial = configuracion["Jwt:Issuer"];
        }

        public string CrearToken(string usuario)
        {
            var cl = new[]
            {
                new Claim(ClaimTypes.Name, usuario),
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_llave));
            var credencial = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _credencial,
                audience: _credencial,
                claims: cl,
                expires: DateTime.UtcNow.AddMinutes(15),
                signingCredentials: credencial
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
