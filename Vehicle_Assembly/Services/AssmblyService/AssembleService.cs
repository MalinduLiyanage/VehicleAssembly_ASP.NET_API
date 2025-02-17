using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Services.AssmblyService;

namespace Vehicle_Assembly.Services.AssembleService
{
    public class AssembleService : IAssembleService
    {
        private readonly ApplicationDbContext context;

        public AssembleService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public BaseResponse GetAssembles(int? vehicle_id, int? worker_id)
        {
            BaseResponse response;
            try
            {
               
                List<AssembleDTO> assembles = new List<AssembleDTO>();

                using (context)
                {
                    var query = context.assembles
                        .Include(a => a.Vehicle)
                        .Include(a => a.Worker)
                        .AsQueryable();

                    if (vehicle_id.HasValue)
                        query = query.Where(a => a.vehicle_id == vehicle_id.Value);

                    if (worker_id.HasValue)
                        query = query.Where(a => a.NIC == worker_id.Value);

                    query.ToList().ForEach(a => assembles.Add(new AssembleDTO
                    {
                        vehicle_id = a.vehicle_id,
                        model = a.Vehicle.model,
                        color = a.Vehicle.color,
                        engine = a.Vehicle.engine,
                        NIC = a.NIC,
                        WorkerName = $"{a.Worker.firstname} {a.Worker.lastname}",
                        job_role = a.Worker.job_role,
                        date = a.date,
                        isCompleted = a.isCompleted
                    }));
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { assembles }
                };
                return response;

            }
            catch (Exception e)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "An error occurred while fetching assemble records." + e.Message }
                };
            }
            return response;

        }

        public BaseResponse CreateAssemble(PutAssembleRequest request)
        {
            BaseResponse response;
            try
            {
                if (request.vehicle_id <= 0 || request.nic <= 0 || request.date == default)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "All fields are required" }
                    };
                    return response;
                }

                var vehicleExists = context.vehicle.Any(v => v.vehicle_id == request.vehicle_id);
                if (!vehicleExists)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "Invalid Vehicle ID. Vehicle does not exist." }
                    };
                    return response;
                }

                var workerExists = context.worker.Any(w => w.NIC == request.nic);
                if (!workerExists)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "Invalid NIC. Worker does not exist." }
                    };
                    return response;
                }

                var newAssemble = new AssembleModel
                {
                    vehicle_id = request.vehicle_id,
                    NIC = request.nic,
                    date = request.date,
                    isCompleted = request.isCompleted
                };

                context.assembles.Add(newAssemble);
                context.SaveChanges();

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Assemble record created successfully!" }
                };
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "An error occurred while creating the assemble record.", error = ex.Message }
                };
            }
            return response;

        }
    }
}
