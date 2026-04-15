using Microsoft.AspNetCore.Http;

namespace CompanySystem.BLL
{
    public class ProductCreateDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }
        public string? Unit { get; set; }
        public bool IsOrganic { get; set; }
        public bool IsFeatured { get; set; }
        public IFormFile? Image { get; set; }
    }
}