
namespace CompanySystem.DAL
{
    public class Product : IAuditableEntity
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public string? ImageURL { get; set; }
        /*------------------------------------------------------------------*/
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        /*------------------------------------------------------------------*/
        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        /*------------------------------------------------------------------*/
    }
}
