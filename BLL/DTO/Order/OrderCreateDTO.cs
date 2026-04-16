namespace CompanySystem.BLL
{
    public class OrderCreateDTO
    {
        public string? PaymentMethod { get; set; } 
        public ShippingAddressDTO? ShippingAddress { get; set; }
    }
}