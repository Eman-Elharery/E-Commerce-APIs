namespace CompanySystem.DAL
{
    public class Category : IAuditableEntity
    {
        /*------------------------------------------------------------------*/
        public int Id { get; set; }
        public required string Name { get; set; }
        /*------------------------------------------------------------------*/
        public virtual ICollection<Product> Products { get; set; }
        = new HashSet<Product>();
        /*------------------------------------------------------------------*/
        // Audit Fields
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        /*------------------------------------------------------------------*/
    }
}
