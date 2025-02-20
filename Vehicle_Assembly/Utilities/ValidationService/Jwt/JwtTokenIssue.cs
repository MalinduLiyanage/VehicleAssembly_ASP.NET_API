using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Utilities.AccountUtility.AdminAccount;
using Vehicle_Assembly.Utilities.EmailService;
using Vehicle_Assembly.Utilities.JwtUtility;

namespace Vehicle_Assembly.Utilities.ValidationService.Jwt
{
    public class JwtTokenIssue
    {
        private readonly ApplicationDbContext context;
        public JwtTokenIssue(ApplicationDbContext context)
        {
            this.context = context;
        }

        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            try
            {
                AdminAccountUtility adminAccountUtility = new AdminAccountUtility();
                AdminModel? user = context.admins.Where(u => u.email == request.email).FirstOrDefault();

                if (user == null)
                {
                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Enter Email and Password!" }
                    };
                }

                if (adminAccountUtility.VerifyPassword(request.password, user.password))
                {
                    string jwt = JwtUtils.GenerateJwtToken(user);
                    /*
                    // save token in login details
                    LoginDetailModel? loginDetail = dbContext.LoginDetails.Where(ld => ld.user_id == user.id).FirstOrDefault();

                    if (loginDetail == null)
                    {
                        loginDetail = new LoginDetailModel();
                        loginDetail.user_id = user.id;
                        loginDetail.token = jwt;

                        dbContext.LoginDetails.Add(loginDetail);
                    }
                    else
                    {
                        loginDetail.token = jwt;
                    }

                    context.SaveChanges();*/

                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { 
                            message = "Logged in Successfully",
                            token = jwt
                        }
                    };
                }
                else
                {
                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new { message = "Invalid username or password" }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse 
                { 
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = ex.Message }
                };
            }
        }


    }
}
