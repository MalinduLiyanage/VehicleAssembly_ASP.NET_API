using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Services.VehicleService
{
    public interface IVehicleService
    {
        BaseResponse GetVehicles();
        
    }
}
