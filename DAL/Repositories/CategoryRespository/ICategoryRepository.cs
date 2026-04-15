namespace CompanySystem.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithProductsAsync();
        Task<Category?> GetByIdWithProductsAsync(int id);
    }
}