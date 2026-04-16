namespace CompanySystem.BLL
{
   
    public class CartReadDTO
    {
        public int CartId { get; set; }
        public IEnumerable<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
        public decimal TotalPrice => Items.Sum(i => i.SubTotal);
    }

}
