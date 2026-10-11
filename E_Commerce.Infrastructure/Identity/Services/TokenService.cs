using E_Commerce.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public class TokenService : ITokenService
    {
        public async Task<string> CreateTokenAsync(string userId, string email, string userName, IReadOnlyList<string> roles)
        {
            //Header[type,algorithm]
            //Payload[claims]
            //Signature[secretkey]


            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,userId),
                new Claim(ClaimTypes.GivenName,userName),
                new Claim(ClaimTypes.Email,email),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }



            var secretKey = "MYSECRETKEYFORMyapplicationMYSECRETKEYFORMyapplicationMYSECRETKEYFORMyapplication";
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwtToken = new JwtSecurityToken(

                issuer: "https://localhost:7289",
                audience: "MyOnlineStore",
                claims:claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256Signature)


                );










            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
