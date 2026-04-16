namespace CompanySystem.BLL
{
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

}