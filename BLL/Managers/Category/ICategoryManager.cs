namespace CompanySystem.BLL
{
    public interface ICategoryManager
    {
        Task<GeneralResult<IEnumerable<CategoryReadDTO>>> GetCategoriesAsync();
        Task<GeneralResult<CategoryReadDTO>> GetCategoryByIdAsync(int id);
        Task<GeneralResult<CategoryReadDTO>> CreateCategoryAsync(CategoryCreateDTO dto);

        Task<GeneralResult<CategoryReadDTO>> UpdateCategoryAsync(int id, CategoryEditDTO dto);

        Task<GeneralResult<bool>> DeleteCategoryAsync(int id);
        Task<GeneralResult<CategoryReadDTO>> UploadCategoryImageAsync(int id,  ImageUploadDto dto, string basePath, string? schema,   string? host);
    }
}
