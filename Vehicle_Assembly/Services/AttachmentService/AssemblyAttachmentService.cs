
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic.FileIO;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Utilities.EmailService;

namespace Vehicle_Assembly.Services.AttachmentService
{
    public class AssemblyAttachmentService : IAssemblyAttachmentService
    {
        
        public async Task PostFileAsync(IFormFile fileData)
        {
            BaseResponse response;
            try
            {

                if (fileData == null || fileData.Length == 0)
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status400BadRequest,
                        data = new { message = "No file has been uploaded." }
                    };
                }

                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, fileData.FileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await fileData.CopyToAsync(stream);
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