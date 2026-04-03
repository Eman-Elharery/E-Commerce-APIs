namespace CompanySystem.BLL
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal => Quantity * UnitPrice;
    }

    public class CartReadDTO
    {
        public int CartId { get; set; }
        public IEnumerable<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public decimal TotalPrice => Items.Sum(i => i.SubTotal);
    }

    public class AddToCartDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateCartItemDTO
    {
        public int ProductId { get; set; }
        public int NewQuantity { get; set; }
    }
}
