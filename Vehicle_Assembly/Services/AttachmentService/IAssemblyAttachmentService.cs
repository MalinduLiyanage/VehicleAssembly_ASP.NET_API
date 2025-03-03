using Microsoft.VisualBasic.FileIO;

namespace Vehicle_Assembly.Services.AttachmentService
{
    public interface IAssemblyAttachmentService
    {
        public Task PostFileAsync(IFormFile fileData);
    }
}
