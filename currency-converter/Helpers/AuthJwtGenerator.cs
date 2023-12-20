using currency_converter.Data.Models.Dto.UserDtos;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace currency_converter.Helpers
{
    public static class AuthJwtGenerator
    {
        public static string GenerateJWT(UserDto user, IConfiguration config, DateTime lifespan)
        {
            SymmetricSecurityKey securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["Authentication:SecretForKey"]!));
            SigningCredentials credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256Signature);

            List<Claim> claimsForToken = new List<Claim>();
            // standard claim names
            claimsForToken.Add(new Claim("sub", user.Id.ToString())); // sub == identity
            claimsForToken.Add(new Claim("given_email", user.Email));
            claimsForToken.Add(new Claim("role", user.Role.ToString()));
            //claimsForToken.Add(new Claim("subscription", user.Subscription.Id.ToString()));

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                config["Authentication:Issuer"],
                config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                lifespan, // token valid for one hour
                credentials
            );

            string tokenToReturn = new JwtSecurityTokenHandler() // stringify token
                .WriteToken(jwtSecurityToken);
            return tokenToReturn;
        }
    }
}
