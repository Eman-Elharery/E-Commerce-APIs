using CompanySystem.DAL;

namespace CompanySystem.BLL
{
    public class OrderManager : IOrderManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<GeneralResult<OrderReadDTO>> PlaceOrderAsync(string userId, OrderCreateDTO dto)
        {
            var cart = await _unitOfWork.CartRepository.GetCartByUserIdAsync(userId);

            if (cart == null || !cart.Items.Any())
                return GeneralResult<OrderReadDTO>.FailResult("Cart is empty");

            var order = new Order
            {
                UserId = userId,
                Status = OrderStatus.Pending,
                PaymentMethod = dto.PaymentMethod,
                TotalPrice = cart.Items.Sum(i => i.Quantity * i.UnitPrice),

                ShippingFullName = dto.ShippingAddress?.FullName,
                ShippingAddress = dto.ShippingAddress?.Address,
                ShippingCity = dto.ShippingAddress?.City,
                ShippingCountry = dto.ShippingAddress?.Country,
                ShippingPhone = dto.ShippingAddress?.Phone,

                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _unitOfWork.OrderRepository.AddAsync(order);
            _unitOfWork.Save();

            await _unitOfWork.CartRepository.ClearCartAsync(userId);

            var created = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(order.Id);
            return GeneralResult<OrderReadDTO>.SuccessResult(MapToDto(created!));
        }


        public async Task<GeneralResult<IEnumerable<OrderReadDTO>>> GetUserOrdersAsync(string userId)
        {
            var orders = await _unitOfWork.OrderRepository.GetOrdersByUserIdAsync(userId);
            return GeneralResult<IEnumerable<OrderReadDTO>>.SuccessResult(orders.Select(MapToDto));
        }


        public async Task<GeneralResult<OrderReadDTO>> GetOrderByIdAsync(string userId, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                return GeneralResult<OrderReadDTO>.NotFound();

            if (order.UserId != userId)
                return GeneralResult<OrderReadDTO>.FailResult("Access denied");

            return GeneralResult<OrderReadDTO>.SuccessResult(MapToDto(order));
        }

        

        public async Task<GeneralResult<OrderReadDTO>> UpdateOrderStatusAsync(int orderId, OrderStatusUpdateDTO dto)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                return GeneralResult<OrderReadDTO>.NotFound();

            if (!Enum.IsDefined(typeof(OrderStatus), dto.NewStatus))
                return GeneralResult<OrderReadDTO>.FailResult("Invalid order status");

            order.Status = (OrderStatus)dto.NewStatus;

            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();

            return GeneralResult<OrderReadDTO>.SuccessResult(MapToDto(order));
        }


        public async Task<GeneralResult<bool>> CancelOrderAsync(string userId, int orderId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderWithItemsAsync(orderId);

            if (order == null)
                return GeneralResult<bool>.NotFound();

            if (order.UserId != userId)
                return GeneralResult<bool>.FailResult("Access denied");

            if (order.Status == OrderStatus.Shipped || order.Status == OrderStatus.Delivered)
                return GeneralResult<bool>.FailResult("Cannot cancel an order that has already been shipped or delivered");

            order.Status = OrderStatus.Cancelled;

            _unitOfWork.OrderRepository.Update(order);
            _unitOfWork.Save();

            return GeneralResult<bool>.SuccessResult(true);
        }


        private static OrderReadDTO MapToDto(Order o) => new()
        {
            Id = o.Id,
            Status = o.Status.ToString(),
            TotalPrice = o.TotalPrice,
            PaymentMethod = o.PaymentMethod,
            CreatedAt = o.CreatedAt,
            ShippingAddress = new ShippingAddressDTO
            {
                FullName = o.ShippingFullName,
                Address = o.ShippingAddress,
                City = o.ShippingCity,
                Country = o.ShippingCountry,
                Phone = o.ShippingPhone
            },
            Items = o.Items.Select(i => new OrderItemReadDTO
            {
                ProductId = i.ProductId,
                ProductTitle = i.Product?.Title ?? string.Empty,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            })
        };
    }
}