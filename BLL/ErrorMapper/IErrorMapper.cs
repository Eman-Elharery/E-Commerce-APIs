using FluentValidation.Results;

namespace CompanySystem.BLL
{
    public interface IErrorMapper
    {
        IEnumerable<string> MapError(ValidationResult validationResult);
    }
}