using CompanySystem.DAL;
using FluentValidation;

namespace CompanySystem.BLL
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductCreateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(p => p.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .WithErrorCode("PRD-01")

                .MinimumLength(3)
                .WithMessage("Title must be at least 3 characters")
                .WithErrorCode("PRD-02")

                .MustAsync(CheckTitleUnique)
                .WithMessage("Title already exists")
                .WithErrorCode("PRD-03");

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

        private async Task<bool> CheckTitleUnique(string title, CancellationToken token)
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return !products.Any(p => p.Title == title);
        }
    } 
}