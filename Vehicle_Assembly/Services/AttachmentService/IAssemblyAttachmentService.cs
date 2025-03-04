using Microsoft.VisualBasic.FileIO;
using Vehicle_Assembly.DTOs.Requests;

namespace Vehicle_Assembly.Services.AttachmentService
{
    public interface IAssemblyAttachmentService
    {
        public Task<string?> PostFileAsync(PutAssembleRequest request);
    }
}
