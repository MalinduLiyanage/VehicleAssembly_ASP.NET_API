using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;

namespace Vehicle_Assembly.Services.WorkerService
{
    public interface IWorkerService
    {
        BaseResponse GetWorkers();

        BaseResponse PutWorker(PutWorkerRequest request);
    }
}
