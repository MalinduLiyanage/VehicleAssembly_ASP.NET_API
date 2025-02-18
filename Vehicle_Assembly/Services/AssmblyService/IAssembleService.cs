using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Services.AssmblyService
{
    public interface IAssembleService
    {
        BaseResponse GetAssembles(int? vehicle_id, int? worker_id, int? assignee_id);
        BaseResponse CreateAssemble(PutAssembleRequest request);
    }
}
