using FluentValidation;

namespace CompanySystem.BLL
{
    public class ProductEditDtoValidator : AbstractValidator<ProductEditDTO>
    {
        public ProductEditDtoValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .WithErrorCode("PRD-01");

            RuleFor(p => p.Price)
                    .GreaterThan(0)
                    .WithMessage("Price must be > 0")
                    .WithErrorCode("PRD-04");

            RuleFor(p => p.Count)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Count must be >= 0")
                    .WithErrorCode("PRD-05");

            RuleFor(p => p.CategoryId)
                    .GreaterThan(0)
                    .WithMessage("Invalid CategoryId")
                    .WithErrorCode("PRD-06");
        }
        

                
    } 
}