namespace CompanySystem.BLL
{
    public interface ICartManager
    {
        Task<GeneralResult<CartReadDTO>> GetCartAsync(string userId);
        Task<GeneralResult<CartReadDTO>> AddToCartAsync(string userId, AddToCartDTO dto);
        Task<GeneralResult<CartReadDTO>> UpdateCartItemAsync(string userId, CartItemUpdateDTO dto);
        Task<GeneralResult<bool>> RemoveFromCartAsync(string userId, int productId);
        Task<GeneralResult<bool>> ClearCartAsync(string userId);
    }
}
