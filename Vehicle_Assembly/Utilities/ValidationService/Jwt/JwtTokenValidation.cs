using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Utilities.JwtUtility;

namespace Vehicle_Assembly.Utilities.ValidationService.Jwt
{
    public class JwtTokenValidation
    {
        private readonly ApplicationDbContext context;
        public JwtTokenValidation(ApplicationDbContext context)
        {
            this.context = context;
        }

        public static bool ValidateJwtToken(string jwt)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] Key = Encoding.ASCII.GetBytes(JwtUtils.Secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken validatedJWT = (JwtSecurityToken)validatedToken;

                string userEmail = validatedJWT.Claims.First(claim => claim.Type == "user_email").Value;

                using (ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    AdminModel? user = dbContext.admins.FirstOrDefault(u => u.email == userEmail);

                    if (user == null)
                    {
                        return false;
                    }
                    else
                    {
                        /*
                        LoginDetailModel loginDetail = dbContext.LoginDetails.Where(ld => ld.user_id == userId).First();

                        if (loginDetail.token != jwt)
                        {
                            return false;
                        }
                        else
                        {
                           
                            return true;
                        }*/
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
