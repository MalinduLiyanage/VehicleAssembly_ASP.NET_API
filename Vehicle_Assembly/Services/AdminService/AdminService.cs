using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Requests.EmailRequests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Utilities.AccountUtility.AdminAccount;
using Vehicle_Assembly.Utilities.EmailService;
using Vehicle_Assembly.Utilities.EmailService.AccountCreation;
using Vehicle_Assembly.Utilities.EmailService.AssemblyEmail;
using Vehicle_Assembly.Utilities.JwtUtility;
using Vehicle_Assembly.Utilities.ValidationService.Jwt;

namespace Vehicle_Assembly.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;

        public AdminService(ApplicationDbContext context, IEmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
        }
        public BaseResponse GetAdmins()
        {
            BaseResponse response;
            try
            {
                List<AdminDTO> admins = new List<AdminDTO>();

                using (context)
                {
                    context.admins.ToList().ForEach(admin => admins.Add(new AdminDTO
                    {
                        NIC = admin.NIC,
                        firstname = admin.firstname,
                        lastname = admin.lastname,
                        email = admin.email,
                    }));
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { admins }
                };
                return response;
            }
            catch (Exception ex) 
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error" + ex.Message }
                };
                return response;
            }
        }

        public BaseResponse PutAdmin(PutAdminRequest request)
        {
            BaseResponse response;
            AdminAccountUtility password = new AdminAccountUtility();
            try
            {
                AdminModel newAdmin = new AdminModel();
                newAdmin.NIC = request.NIC;
                newAdmin.firstname = request.firstname;
                newAdmin.lastname = request.lastname;
                newAdmin.email = request.email;
                newAdmin.password = password.PasswordHash;

                AdminAccountEmailRequest AccCreationRequest = new AdminAccountEmailRequest
                {
                    NIC = request.NIC,
                    firstname = request.firstname,
                    lastname = request.lastname,
                    email = request.email,
                    password = password.generatedPassword
                };

                AdminAccountCreation message = new AdminAccountCreation(AccCreationRequest);

                using (context)
                {
                    context.Add(newAdmin);
                    context.SaveChanges();
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { 
                        message = "Successfully created a Admin Record",
                        email_status = emailService.SendEmail(message)
                    }

                };
                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error" + ex.Message }
                };
                return response;
            }
        }

        public BaseResponse LoginAdmin(AuthenticateRequest request)
        {
            BaseResponse response;
            try
            {
                JwtTokenIssue jwtTokenIssue = new JwtTokenIssue(context);
                return jwtTokenIssue.Authenticate(request);
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
