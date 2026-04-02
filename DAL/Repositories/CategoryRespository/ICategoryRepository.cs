namespace CompanySystem.DAL
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithProductsAsync();
    }
}