using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Vehicle_Assembly.DTOs.Requests;
using Vehicle_Assembly.DTOs.Responses;
using Vehicle_Assembly.Models;
using Vehicle_Assembly.Utilities.ValidationService.AssembleRequest;

namespace Vehicle_Assembly.Attributes
{
    public class AssembleRequestValidationAttribute : ValidationAttribute
    {

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var request = (PutAssembleRequest)validationContext.ObjectInstance;

            if (request.vehicle_id <= 0 || request.nic <= 0 || request.date == default)
            {
                return new ValidationResult("All fields are required.");
            }

            IAssembleRequestValidationService validationService = (IAssembleRequestValidationService)validationContext.GetService(typeof(IAssembleRequestValidationService));

            if (validationService == null)
            {
                return new ValidationResult("Validation service is unavailable.");
            }

            var validationErrors = validationService.Validate(request);

            if (validationErrors.Any())
            {
                return new ValidationResult(string.Join("; ", validationErrors));
            }

            return ValidationResult.Success;
        }
    }
}

