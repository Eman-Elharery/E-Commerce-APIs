namespace CompanySystem.BLL
{
    public class OrderItemReadDTO
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }

    public class OrderReadDTO
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string? ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderItemReadDTO> Items { get; set; } = new List<OrderItemReadDTO>();
    }

    public class CreateOrderDTO
    {
        public string? ShippingAddress { get; set; }
    }

    public class UpdateOrderStatusDTO
    {
        public int NewStatus { get; set; }
    }
}
