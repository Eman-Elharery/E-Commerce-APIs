using FluentValidation.Results;

namespace CompanySystem.BLL
{
    public class ErrorMapper : IErrorMapper
    {
        public IEnumerable<string> MapError(ValidationResult validationResult)
        {
            return validationResult.Errors
                .Select(e => $"{e.ErrorCode} : {e.ErrorMessage}");
        }
    }
}