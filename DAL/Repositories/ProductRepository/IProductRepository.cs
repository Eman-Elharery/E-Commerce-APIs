namespace CompanySystem.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithCategoryAsync();

        Task<Product?> GetByIdWithCategoryAsync(int id);
    }
}