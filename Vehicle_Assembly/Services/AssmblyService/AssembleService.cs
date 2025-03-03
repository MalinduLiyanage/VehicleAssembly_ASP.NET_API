using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Requests.EmailRequests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Services.AssmblyService;
using Vehicle_Assembly.Services.AttachmentService;
using Vehicle_Assembly.Utilities.EmailService;
using Vehicle_Assembly.Utilities.EmailService.AssemblyEmail;

namespace Vehicle_Assembly.Services.AssembleService
{
    public class AssembleService : IAssembleService
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;
        private readonly IAssemblyAttachmentService assemblyAttachmentService;

        public AssembleService(ApplicationDbContext context, IEmailService emailService, IAssemblyAttachmentService assemblyAttachmentService)
        {
            this.context = context;
            this.emailService = emailService;
            this.assemblyAttachmentService = assemblyAttachmentService;
        }

        public BaseResponse GetAssembles(int? vehicle_id, int? worker_id, int? assignee_id)
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
                        .Include(a => a.Admin)
                        .AsQueryable();

                    if (vehicle_id.HasValue)
                        query = query.Where(a => a.vehicle_id == vehicle_id.Value);

                    if (worker_id.HasValue)
                        query = query.Where(a => a.NIC == worker_id.Value);

                    if (assignee_id.HasValue)
                        query = query.Where(a => a.assignee_id == assignee_id.Value);

                    query.ToList().ForEach(a => assembles.Add(new AssembleDTO
                    {
                        assignee_id = a.assignee_id,
                        assignee_first_name = a.Admin.firstname,
                        assignee_last_name = a.Admin.lastname,
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

                AssembleModel newAssemble = new AssembleModel
                {
                    assignee_id = request.assignee_id,
                    vehicle_id = request.vehicle_id,
                    NIC = request.nic,
                    date = request.date,
                    isCompleted = request.isCompleted
                };

                AssemblyAttachmentService attachFile = new AssemblyAttachmentService();
                attachFile.PostFileAsync(request.assembly_attachment);

                context.assembles.Add(newAssemble);
                context.SaveChanges();

                VehicleModel? vehicle = context.vehicle.FirstOrDefault(v => v.vehicle_id == request.vehicle_id);
                WorkerModel? worker = context.worker.FirstOrDefault(w => w.NIC == request.nic);
                AdminModel? admin = context.admins.FirstOrDefault(a => a.NIC == request.assignee_id);

                EmailDTO emailInfo = new EmailDTO
                {
                    assignee_id = request.assignee_id,
                    assignee_first_name = admin.firstname,
                    assignee_last_name = admin.lastname,
                    vehicle_id = vehicle.vehicle_id,
                    model = vehicle.model,
                    color = vehicle.color,
                    engine = vehicle.engine,
                    NIC = worker.NIC,
                    WorkerName = $"{worker.firstname} {worker.lastname}",
                    email = worker.email,
                    job_role = worker.job_role,
                    date = request.date
                };

                SendEmailRequest emailRequest = new SendEmailRequest
                {
                    request = emailInfo
                };

                AssemblyEmailSender message = new AssemblyEmailSender(emailRequest);

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new 
                    { 
                        message = "Assemble record created successfully!" ,
                        email_status = emailService.SendEmail(message)
                    }
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
