using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using celsia.Services.Interfaces;
using celsia.Models;
using celsia.Data;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace celsia.Services.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly JwtSettings _jwtSettings;

        public AuthRepository(DataContext context, IOptions<JwtSettings> options)
        {
            _context = context;
            _jwtSettings = options.Value;
        }

        public string GenerateToken(Administrador user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Correo),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // new Claim(ClaimTypes.Role, user.Rol)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.SecurityKey,
                audience: _jwtSettings.SecurityKey,
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Administrador> GetAll()
        {
            throw new NotImplementedException();
        }

        public Administrador Login(string email, string Password)
        {
            Console.WriteLine("AQUI---------------!!!!!");
            Console.WriteLine(email);

            if (string.IsNullOrEmpty(email))
            {
                return null; // 
            }

            //var user = _context.MarketingUsers.FirstOrDefault(u => u.Username == Username);
            var user = _context.Administradores.FirstOrDefault(u => u.Correo.ToLower() == email.ToLower());

            if (user != null && user.Contraseña.ToLower() == Password.ToLower())
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public void LogOutAsync()
        {
            throw new NotImplementedException();
        }
        // gSI=eFk4G3ZRy`(Kg£+<X(1VI4)5=RKw
        // 9Y9+JKvPyNR0qmUGeCT1oHfCwK2E4EK9YiUCRLXL9D8="
    }
}