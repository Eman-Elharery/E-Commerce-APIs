namespace CompanySystem.BLL
{
    public class ProductReadDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
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
        public string? CategoryName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}