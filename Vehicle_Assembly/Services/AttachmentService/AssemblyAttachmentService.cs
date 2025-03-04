
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic.FileIO;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Utilities.EmailService;

namespace Vehicle_Assembly.Services.AttachmentService
{
    public class AssemblyAttachmentService : IAssemblyAttachmentService
    {
        
        public async Task PostFileAsync(PutAssembleRequest request)
        {
            BaseResponse response;
            try
            {

                if (request.assembly_attachment == null || request.assembly_attachment.Length == 0)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No file has been uploaded." }
                    };
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileExtension = Path.GetExtension(request.assembly_attachment.FileName);
                var filename = request.assignee_id + "_" + request.vehicle_id + "_" + request.nic + fileExtension;

                var filePath = Path.Combine(uploadsFolder, filename);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.assembly_attachment.CopyToAsync(stream);
                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new
                    {
                        message = "File uploaded successfully!"
                    }
                };
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "An error occurred while uploading.", error = ex.Message }
                };
            }
        }
    }
}