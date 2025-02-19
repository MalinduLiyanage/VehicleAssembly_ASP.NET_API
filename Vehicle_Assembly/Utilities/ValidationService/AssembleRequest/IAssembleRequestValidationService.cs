using Vehicle_Assembly.DTOs.Requests;

namespace Vehicle_Assembly.Utilities.ValidationService.AssembleRequest
{
    public interface IAssembleRequestValidationService
    {
        List<string> Validate(PutAssembleRequest request);
    }
}
