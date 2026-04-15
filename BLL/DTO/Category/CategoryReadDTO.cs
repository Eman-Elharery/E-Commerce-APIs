namespace CompanySystem.BLL
{
    public class CategoryReadDTO
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? Slug { get; set; }
        public int ProductCount { get; set; }
    }
}