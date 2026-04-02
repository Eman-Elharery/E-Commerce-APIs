using CompanySystem.DAL;

namespace CompanySystem.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IErrorMapper _errorMapper;
        public CategoryManager(
            IUnitOfWork unitOfWork,
            IErrorMapper errorMapper)
        {
            _unitOfWork = unitOfWork;
            
            _errorMapper = errorMapper;
        }

        public async Task<GeneralResult<IEnumerable<CategoryReadDTO>>> GetCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            var result = categories.Select(c => new CategoryReadDTO
            {
                Id = c.Id,
                Name = c.Name
            });

            return GeneralResult<IEnumerable<CategoryReadDTO>>.SuccessResult(result);
        }

        public async Task<GeneralResult<CategoryReadDTO>> CreateCategoryAsync(CategoryCreateDTO dto)
        {

            var category = new Category
            {
                Name = dto.Name!
            };

            _unitOfWork.CategoryRepository.AddAsync(category);

            _unitOfWork.Save();

            var result = new CategoryReadDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            return GeneralResult<CategoryReadDTO>.SuccessResult(result);
        }



        public async Task<GeneralResult<CategoryReadDTO>> UpdateCategoryAsync(int id, CategoryEditDTO dto)
        {


            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            if (category == null)
                return GeneralResult<CategoryReadDTO>.NotFound();

            category.Name = dto.Name!;

            _unitOfWork.CategoryRepository.Update(category);

             _unitOfWork.Save();

            var result = new CategoryReadDTO
            {
                Id = category.Id,
                Name = category.Name
            };

            return GeneralResult<CategoryReadDTO>.SuccessResult(result);
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
    }
}