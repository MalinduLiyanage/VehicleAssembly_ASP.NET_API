using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Utilities.JwtUtility
{
    public static class JwtUtils
    {
        static string secret = "9wr4734oXQrf0o50y9eA4onr734yriV87";
        public static string Secret => secret;

        public static string GenerateJwtToken(AdminModel userModel) 
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secret);

            List<Claim> claims = new List<Claim>
            {
                new Claim("user_email", userModel.email),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }

    }
}
