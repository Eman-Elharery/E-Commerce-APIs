using Microsoft.AspNetCore.Identity;

namespace CompanySystem.DAL
{
   
    public class ApplicationUser : IdentityUser, IAuditableEntity
    {
        public required string FirstName { get; set; }
        public required string LastName  { get; set; }
        public DateTime  CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public string FullName => $"{FirstName} {LastName}";
    }
}
