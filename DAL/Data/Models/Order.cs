namespace CompanySystem.DAL
{
    public class Order : IAuditableEntity
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        public required string UserId { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalPrice { get; set; }
        public string? PaymentMethod { get; set; }  // "cod" | "credit_card"
        /*------------------------------------------------------------------*/
        public string? ShippingFullName { get; set; }
        public string? ShippingAddress { get; set; }
        public string? ShippingCity { get; set; }
        public string? ShippingCountry { get; set; }
        public string? ShippingPhone { get; set; }
        /*------------------------------------------------------------------*/
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<OrderItem> Items { get; set; }
            = new HashSet<OrderItem>();
        /*------------------------------------------------------------------*/
        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        /*------------------------------------------------------------------*/
    }
}