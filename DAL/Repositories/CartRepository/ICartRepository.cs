namespace CompanySystem.DAL
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        Task<Cart?> GetCartByUserIdAsync(string userId);
        Task ClearCartAsync(string userId);
    }
}
