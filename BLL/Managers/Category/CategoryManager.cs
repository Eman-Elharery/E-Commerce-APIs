using CompanySystem.DAL;

namespace CompanySystem.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorMapper _errorMapper;
        private readonly IImageManager _imageManager;

        public CategoryManager(
            IUnitOfWork unitOfWork,
            IErrorMapper errorMapper,
            IImageManager imageManager)
        {
            _unitOfWork = unitOfWork;
            _errorMapper = errorMapper;
            _imageManager = imageManager;
        }

        

        public async Task<GeneralResult<IEnumerable<CategoryReadDTO>>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllWithProductsAsync();

            return GeneralResult<IEnumerable<CategoryReadDTO>>.SuccessResult(categories.Select(MapToDto));
        }


        public async Task<GeneralResult<CategoryReadDTO>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id);

            if (category == null)
                return GeneralResult<CategoryReadDTO>.NotFound();

            return GeneralResult<CategoryReadDTO>.SuccessResult(MapToDto(category));
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


        public async Task<GeneralResult<CategoryReadDTO>> UploadCategoryImageAsync(
            int id,
            ImageUploadDto dto,
            string basePath,
            string schema,
            string host)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdWithProductsAsync(id);

            if (category == null)
                return GeneralResult<CategoryReadDTO>.NotFound();

            var uploadResult = await _imageManager.UploadAsync(dto, basePath, schema, host);

            if (!uploadResult.Success)
                return GeneralResult<CategoryReadDTO>.FailResult(uploadResult.Message ?? "Image upload failed");

            category.ImageURL = uploadResult.Data!.ImageURL;

            _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Save();

            return GeneralResult<CategoryReadDTO>.SuccessResult(MapToDto(category));
        }

        

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