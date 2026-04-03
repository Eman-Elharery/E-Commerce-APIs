namespace CompanySystem.DAL
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<Order?> GetOrderWithItemsAsync(int orderId);
    }
}
