using Microsoft.AspNetCore.Mvc;

namespace JewelArchitecture.Examples.SmartCharging.Interface.Shared;

public class BusinessValidationHelper
{
    public static ValidationProblemDetails GetValidationProblemDetails(string errorKey, string validationError)
    {
        var errorDetails = new ValidationProblemDetails
        {
            Title = "Business validation failed",
            Detail = "Please refer to the errors property for additional details."
        };

        errorDetails.Errors.Add(errorKey, [validationError]);

        return errorDetails;
    }
}
