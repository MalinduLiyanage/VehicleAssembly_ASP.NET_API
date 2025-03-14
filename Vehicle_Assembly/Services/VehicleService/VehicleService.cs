﻿using Microsoft.EntityFrameworkCore;
using Vehicle_Assembly.DTOs;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;

namespace Vehicle_Assembly.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly ApplicationDbContext context;

        public VehicleService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public BaseResponse GetVehicles() 
        {
            BaseResponse response;
            try
            {
                List<VehicleDTO> vehicles = new List<VehicleDTO>();

                using (context) 
                {
                    context.vehicle.ToList().ForEach(vehicle => vehicles.Add(new VehicleDTO
                    {
                        vehicle_id = vehicle.vehicle_id,
                        model = vehicle.model,
                        color = vehicle.color,
                        engine = vehicle.engine,
                    }));
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { vehicles }
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

        public BaseResponse PutVehicles(PutVehicleRequest request)
        {
            BaseResponse response;
            try
            {
                VehicleModel newVehicle = new VehicleModel();
                newVehicle.model = request.model;
                newVehicle.color = request.color;
                newVehicle.engine = request.engine;


                using (context)
                {
                    context.Add(newVehicle);
                    context.SaveChanges();
                }
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created a Vehicle Record" }
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
