﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Services.AssmblyService;
using Vehicle_Assembly.Services.EmailService;

namespace Vehicle_Assembly.Services.AssembleService
{
    public class AssembleService : IAssembleService
    {
        private readonly ApplicationDbContext context;
        private readonly IEmailService emailService;

        public AssembleService(ApplicationDbContext context, IEmailService emailService)
        {
            this.context = context;
            this.emailService = emailService;
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
                        .Include(a => a.Admin)
                        .AsQueryable();

                    if (vehicle_id.HasValue)
                        query = query.Where(a => a.vehicle_id == vehicle_id.Value);

                    if (worker_id.HasValue)
                        query = query.Where(a => a.NIC == worker_id.Value);

                    query.ToList().ForEach(a => assembles.Add(new AssembleDTO
                    {
                        assignee_id = a.assignee_id,
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

                VehicleModel vehicle = context.vehicle.FirstOrDefault(v => v.vehicle_id == request.vehicle_id);
                if (vehicle == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "Invalid Vehicle ID. Vehicle does not exist." }
                    };
                    return response;
                }

                WorkerModel worker = context.worker.FirstOrDefault(w => w.NIC == request.nic);
                if (worker == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "Invalid NIC. Worker does not exist." }
                    };
                    return response;
                }

                AdminModel admin = context.admins.FirstOrDefault(a => a.NIC == request.assignee_id);
                if (admin == null)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status404NotFound,
                        data = new { message = "Invalid Assignee ID. Admin does not exist." }
                    };
                    return response;
                }

                AssembleModel newAssemble = new AssembleModel
                {
                    assignee_id = request.assignee_id,
                    vehicle_id = request.vehicle_id,
                    NIC = request.nic,
                    date = request.date,
                    isCompleted = request.isCompleted
                };

                context.assembles.Add(newAssemble);
                context.SaveChanges();       

                SendEmailRequest emailRequest = new SendEmailRequest 
                { 
                    recipientName = $"{worker.firstname} {worker.lastname}",
                    recipientEmail = worker.email,
                    subject = "Assemble Job Assignment - " + request.date,
                    body = "You are assigned to the Vehicle : " + " " + vehicle.model + " with the ID " + request.vehicle_id + 
                            " which is scheduled to be completed on " + request.date 
                            + " Assigned By Admin : " + $"{admin.firstname} {admin.lastname} with the ID " + request.assignee_id

                };

                emailService.SendEmail(emailRequest);

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
