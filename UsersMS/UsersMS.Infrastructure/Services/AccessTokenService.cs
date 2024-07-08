using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UsersMS.Infrastructure.Domain.Entities;

namespace UsersMS.Infrastructure.Services
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersDataLayer _usersDataLayer;

        public AccessTokenService(IUsersDataLayer usersDataLayer)
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            _usersDataLayer = usersDataLayer;
        }
        public string GenerateAccessTokenFromUser(User userByEmail)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, userByEmail.Email),
                new Claim(ClaimTypes.Name, userByEmail.Name),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> ValidateAccessToken(string jwtToken)
        {
            if(jwtToken == null)
                return false;

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            try
            {
                tokenHandler.ValidateToken(jwtToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                JwtSecurityToken token = (JwtSecurityToken)validatedToken;
                string userEmail = token.Claims.First(x => x.Type == ClaimTypes.Email).Value;

                User user = await _usersDataLayer.GetUserByEmail(userEmail);
                if (user.Id < 0)
                    return false;

                return true;

            } catch (Exception ex)
            {
                return false;
            }
        }
    }
}
