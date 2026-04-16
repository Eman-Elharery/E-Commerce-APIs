namespace CompanySystem.BLL
{
    public interface IOrderManager
    {
        Task<GeneralResult<OrderReadDTO>>              PlaceOrderAsync(string userId, OrderCreateDTO dto);
        Task<GeneralResult<IEnumerable<OrderReadDTO>>> GetUserOrdersAsync(string userId);
        Task<GeneralResult<OrderReadDTO>>              GetOrderByIdAsync(string userId, int orderId);
        Task<GeneralResult<OrderReadDTO>>              UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDTO dto);
        Task<GeneralResult<bool>>                      CancelOrderAsync(string userId, int orderId);
    }
}
