namespace CompanySystem.BLL
{
    public class CategoryCreateDTO
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public string? Slug { get; set; }
    }
}