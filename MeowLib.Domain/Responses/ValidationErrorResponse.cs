using MeowLib.Domain.Models;

namespace MeowLib.Domain.Responses;

public class ValidationErrorResponse : BaseErrorResponse
{
    public IEnumerable<ValidationErrorModel> ValidationErrors { get; set; }

    public ValidationErrorResponse(IEnumerable<ValidationErrorModel> validationErrors) : base("Ошибка валидации данных")
    {
        ValidationErrors = validationErrors;
    }
}