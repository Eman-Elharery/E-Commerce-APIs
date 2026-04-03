namespace CompanySystem.BLL
{
    public interface IOrderManager
    {
        Task<GeneralResult<OrderReadDTO>>              PlaceOrderAsync(string userId, CreateOrderDTO dto);
        Task<GeneralResult<IEnumerable<OrderReadDTO>>> GetUserOrdersAsync(string userId);
        Task<GeneralResult<OrderReadDTO>>              GetOrderByIdAsync(string userId, int orderId);
        Task<GeneralResult<OrderReadDTO>>              UpdateOrderStatusAsync(int orderId, UpdateOrderStatusDTO dto);
        Task<GeneralResult<bool>>                      CancelOrderAsync(string userId, int orderId);
    }
}
