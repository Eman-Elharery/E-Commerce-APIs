using Microsoft.AspNetCore.Identity;

namespace CompanySystem.DAL
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
    }
}
