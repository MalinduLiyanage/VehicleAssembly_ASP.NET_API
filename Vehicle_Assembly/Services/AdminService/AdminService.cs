using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext context;
        public AdminService(ApplicationDbContext context)
        {
            this.context = context;
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
            try
            {
                AdminModel newAdmin = new AdminModel();
                newAdmin.NIC = request.NIC;
                newAdmin.firstname = request.firstname;
                newAdmin.lastname = request.lastname;
                newAdmin.email = request.email;

                using (context)
                {
                    context.Add(newAdmin);
                    context.SaveChanges();
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created a Admin Record" }
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
    }
}
