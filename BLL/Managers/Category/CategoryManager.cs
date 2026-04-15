using CompanySystem.DAL;

namespace CompanySystem.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorMapper _errorMapper;

        public CategoryManager(IUnitOfWork unitOfWork, IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            _errorMapper = errorMapper;
        }


        public async Task<GeneralResult<IEnumerable<CategoryReadDTO>>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            var result = categories.Select(MapToDto);

            return GeneralResult<IEnumerable<CategoryReadDTO>>.SuccessResult(result);
        }

        public async Task<GeneralResult<CategoryReadDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id);

            if (category == null)
                return GeneralResult<CategoryReadDTO>.NotFound();

            var result = new CategoryReadDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageURL = category.ImageURL,
                Slug = category.Slug,
                ProductCount = category.Products?.Count ?? 0
            };

            return GeneralResult<CategoryReadDTO>.SuccessResult(result);
        }
        public async Task<GeneralResult<CategoryReadDTO>> CreateCategoryAsync(CategoryCreateDTO dto)
        {
            var category = new Category
            {
                Name = dto.Name!,
                Description = dto.Description,
                ImageURL = dto.ImageURL,
                Slug = dto.Slug
            };

            _unitOfWork.CategoryRepository.AddAsync(category);
            _unitOfWork.Save();

            return GeneralResult<CategoryReadDTO>.SuccessResult(MapToDto(category));
        }


        public async Task<GeneralResult<CategoryReadDTO>> UpdateCategoryAsync(int id, CategoryEditDTO dto)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return GeneralResult<CategoryReadDTO>.NotFound();

            category.Name = dto.Name!;
            category.Description = dto.Description;
            category.ImageURL = dto.ImageURL;
            category.Slug = dto.Slug;

            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();

            return GeneralResult<CategoryReadDTO>.SuccessResult(MapToDto(category));
        }


        public async Task<GeneralResult<bool>> DeleteCategoryAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return GeneralResult<bool>.NotFound();

            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Save();

            return GeneralResult<bool>.SuccessResult(true);
        }

        /*------------------------------------------------*/

        private static CategoryReadDTO MapToDto(Category c) => new()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            ImageURL = c.ImageURL,
            Slug = c.Slug,
            ProductCount = c.Products?.Count ?? 0
        };

        
    }
}