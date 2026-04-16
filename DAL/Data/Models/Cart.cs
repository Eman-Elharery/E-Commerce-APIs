namespace CompanySystem.DAL
{
    public class Cart : IAuditableEntity
    {
        public int Id { get; set; }
        public required string UserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;
        public virtual ICollection<CartItem> Items { get; set; }
            = new HashSet<CartItem>();
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
