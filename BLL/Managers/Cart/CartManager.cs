using CompanySystem.DAL;

namespace CompanySystem.BLL
{
    public class CartManager : ICartManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<GeneralResult<CartReadDTO>> GetCartAsync(string userId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return GeneralResult<CartReadDTO>.SuccessResult(new CartReadDTO());

            return GeneralResult<CartReadDTO>.SuccessResult(MapToDto(cart));
        }


        public async Task<GeneralResult<CartReadDTO>> AddToCartAsync(string userId, AddToCartDTO dto)
        {
            if (dto.Quantity <= 0)
                return GeneralResult<CartReadDTO>.FailResult("Quantity must be greater than 0");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(dto.ProductId);

            if (product == null)
                return GeneralResult<CartReadDTO>.FailResult("Product not found");

            if (product.Count < dto.Quantity)
                return GeneralResult<CartReadDTO>.FailResult("Not enough stock available");

            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart { UserId = userId };
                await _unitOfWork.CartRepository.AddAsync(cart);
                _unitOfWork.Save();
                cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            }

            var existingItem = cart!.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    CartId    = cart.Id,
                    ProductId = dto.ProductId,
                    Quantity  = dto.Quantity,
                    UnitPrice = product.Price
                });
            }

            _unitOfWork.CartRepository.Update(cart);
            _unitOfWork.Save();

            var updated = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            return GeneralResult<CartReadDTO>.SuccessResult(MapToDto(updated!));
        }


        public async Task<GeneralResult<CartReadDTO>> UpdateCartItemAsync(string userId, CartItemUpdateDTO dto)
        {
            if (dto.NewQuantity <= 0)
                return GeneralResult<CartReadDTO>.FailResult("Quantity must be greater than 0");

            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return GeneralResult<CartReadDTO>.NotFound();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);

            if (item == null)
                return GeneralResult<CartReadDTO>.FailResult("Product not found in cart");

            item.Quantity = dto.NewQuantity;

            _unitOfWork.CartRepository.Update(cart);
            _unitOfWork.Save();

            var updated = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);
            return GeneralResult<CartReadDTO>.SuccessResult(MapToDto(updated!));
        }


        public async Task<GeneralResult<bool>> RemoveFromCartAsync(string userId, int productId)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
                return GeneralResult<bool>.NotFound();

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (item == null)
                return GeneralResult<bool>.FailResult("Product not found in cart");

            cart.Items.Remove(item);

            _unitOfWork.CartRepository.Update(cart);
            _unitOfWork.Save();

            return GeneralResult<bool>.SuccessResult(true);
        }


        public async Task<GeneralResult<bool>> ClearCartAsync(string userId)
        {
            await _unitOfWork.CartRepository.ClearCartAsync(userId);
            return GeneralResult<bool>.SuccessResult(true);
        }


        private static CartReadDTO MapToDto(Cart cart)
        {
            return new CartReadDTO
            {
                CartId = cart.Id,
                Items  = cart.Items.Select(i => new CartItemDTO
                {
                    ProductId    = i.ProductId,
                    ProductTitle = i.Product?.Title ?? string.Empty,
                    Quantity     = i.Quantity,
                    UnitPrice    = i.UnitPrice
                })
            };
        }
    }
}
