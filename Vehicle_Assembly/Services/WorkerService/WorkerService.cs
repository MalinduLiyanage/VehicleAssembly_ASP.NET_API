using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Services.WorkerService
{
    public class WorkerService : IWorkerService
    {
        private readonly ApplicationDbContext context;

        public WorkerService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public BaseResponse GetWorkers()
        {
            BaseResponse response;
            try
            {
                List<WorkerDTO> workers = new List<WorkerDTO>();

                using (context)
                {
                    context.worker.ToList().ForEach(worker => workers.Add(new WorkerDTO
                    {
                        NIC = worker.NIC,
                        firstname = worker.firstname,
                        lastname = worker.lastname,
                        email = worker.email,
                        address = worker.address,
                        job_role = worker.job_role,
                    }));
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { workers }
                };
                return response;
            }
            catch (Exception e)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error" + e.Message }
                };
                return response;

            }
        }

        public BaseResponse PutWorker(PutWorkerRequest request)
        {
            BaseResponse response;
            try
            {
                WorkerModel newWorker = new WorkerModel();
                newWorker.NIC = request.NIC;
                newWorker.firstname = request.firstname;
                newWorker.lastname = request.lastname;
                newWorker.email = request.email;
                newWorker.address = request.address;
                newWorker.job_role = request.job_role;

                using (context)
                {
                    context.Add(newWorker);
                    context.SaveChanges();
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created a Worker Record" }
                };
                return response;
            }
            catch (Exception e)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal Server Error" + e.Message }
                };
                return response;

            }
        }
    }
}
