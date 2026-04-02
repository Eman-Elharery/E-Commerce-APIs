using CompanySystem.BLL;
using CompanySystem.DAL;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySystem.BLL
{
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryCreateDtoValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .WithErrorCode("CAT-01")

                .MinimumLength(3)
                .WithMessage("Name must be at least 3 characters")
                .WithErrorCode("CAT-02")

                .MustAsync(CheckNameUnique)
                .WithMessage("Category already exists")
                .WithErrorCode("CAT-03");
        }

        private async Task<bool> CheckNameUnique(string name, CancellationToken token)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return !categories.Any(c => c.Name == name);
        }
    }
}
