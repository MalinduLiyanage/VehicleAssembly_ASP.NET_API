using Vehicle_Assembly.DTOs.Requests;

namespace Vehicle_Assembly.Services.ValidationService.AssembleRequest
{
    public interface IAssembleRequestValidationService
    {
        List<string> Validate(PutAssembleRequest request);
    }
}
