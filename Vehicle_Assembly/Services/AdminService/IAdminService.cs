using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Services.AdminService
{
    public interface IAdminService
    {
        BaseResponse GetAdmins();
        BaseResponse PutAdmin(PutAdminRequest request);
        BaseResponse LoginAdmin(AuthenticateRequest request);
    }
}
