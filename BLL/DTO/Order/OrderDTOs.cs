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

    /*------------------------------------------------------------------*/
    public class ShippingAddressDTO
    {
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
    }

    /*------------------------------------------------------------------*/
    public class OrderReadDTO
    {
        public int Id { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public string? PaymentMethod { get; set; }
        public ShippingAddressDTO? ShippingAddress { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<OrderItemReadDTO> Items { get; set; }
            = new List<OrderItemReadDTO>();
    }

    /*------------------------------------------------------------------*/
    public class CreateOrderDTO
    {
        public string? PaymentMethod { get; set; }  // "cod" | "credit_card"
        public ShippingAddressDTO? ShippingAddress { get; set; }
    }

    /*------------------------------------------------------------------*/
    public class UpdateOrderStatusDTO
    {
        public int NewStatus { get; set; }
    }
}