namespace CompanySystem.DAL
{
    public class Category : IAuditableEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? Slug { get; set; }        
        public virtual ICollection<Product> Products { get; set; }
            = new HashSet<Product>();
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}