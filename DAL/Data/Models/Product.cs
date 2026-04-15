namespace CompanySystem.DAL
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        public string? Unit { get; set; }        
        public double Rating { get; set; }
        public int Reviews { get; set; }
        public bool IsOrganic { get; set; }
        public bool IsFeatured { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}