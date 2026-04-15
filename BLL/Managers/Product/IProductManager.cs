using CompanySystem.Common;

namespace CompanySystem.BLL
{
    public interface IProductManager
    {
        Task<GeneralResult<PagedResult<ProductReadDTO>>> GetProductsAsync(
            ProductFilterParameters filter,
            PaginationParameters pagination);
        Task<GeneralResult<ProductReadDTO>> GetProductByIdAsync(int id);

        Task<GeneralResult<ProductReadDTO>> CreateProductAsync(ProductCreateDTO dto);

        Task<GeneralResult<ProductReadDTO>> UpdateProductAsync(int id, ProductEditDTO dto);

        Task<GeneralResult<bool>> DeleteProductAsync(int id);

    }
}
