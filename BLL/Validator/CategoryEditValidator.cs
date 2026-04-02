
using FluentValidation;

namespace CompanySystem.BLL
{
    public class CategoryEditDtoValidator : AbstractValidator<CategoryEditDTO>
    {

        public CategoryEditDtoValidator()
        {

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .WithErrorCode("CAT-01")

                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters")
                .WithErrorCode("CAT-02");

                
        }

    }
}
