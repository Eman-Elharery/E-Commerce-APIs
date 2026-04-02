using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace CompanySystem.BLL
{
    public class ProductCreateDTO
    {
        public string? Title { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public int CategoryId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
