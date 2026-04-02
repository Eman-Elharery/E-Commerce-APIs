using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CompanySystem.DAL
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private static readonly DateTime _seedDate = new DateTime(2026, 3, 1, 10, 30, 0);

        public AppDbContext() : base() { }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public override int SaveChanges()
        {
            AuditLog();
            return base.SaveChanges();
        }

        private void AuditLog()
        {
            var dateTime = DateTime.UtcNow;
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = dateTime;
                else if (entry.State == EntityState.Modified)
                    entry.Entity.UpdatedAt = dateTime;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole
                {
                    Id = "role-admin-guid-0001",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "role-admin-stamp-0001",
                    Description = "Full system access"
                },
                new ApplicationRole
                {
                    Id = "role-user-guid-0002",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "role-user-stamp-0002",
                    Description = "Read-only access"
                }
            );

            var hasher = new PasswordHasher<ApplicationUser>();
            var adminId = "user-admin-guid-0001";

            var adminUser = new ApplicationUser
            {
                Id = adminId,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@company.com",
                NormalizedEmail = "ADMIN@COMPANY.COM",
                FirstName = "System",
                LastName = "Admin",
                EmailConfirmed = true,
                SecurityStamp = "admin-security-stamp-fixed",
                ConcurrencyStamp = "admin-concurrency-stamp-fixed",
                CreatedAt = _seedDate  
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = adminId,
                    RoleId = "role-admin-guid-0001"
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", CreatedAt = _seedDate },
                new Category { Id = 2, Name = "Clothes", CreatedAt = _seedDate },
                new Category { Id = 3, Name = "Books", CreatedAt = _seedDate },
                new Category { Id = 4, Name = "Home Appliances", CreatedAt = _seedDate },
                new Category { Id = 5, Name = "Sports", CreatedAt = _seedDate },
                new Category { Id = 6, Name = "Accessories", CreatedAt = _seedDate }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Title = "Laptop", Description = "Gaming Laptop", Price = 15000, Count = 5, CategoryId = 1, CreatedAt = _seedDate },
                new Product { Id = 2, Title = "Smartphone", Description = "Android Smartphone", Price = 9000, Count = 10, CategoryId = 1, CreatedAt = _seedDate },
                new Product { Id = 3, Title = "Headphones", Description = "Wireless Headphones", Price = 1200, Count = 15, CategoryId = 1, CreatedAt = _seedDate },
                new Product { Id = 4, Title = "T-Shirt", Description = "Cotton T-Shirt", Price = 300, Count = 20, CategoryId = 2, CreatedAt = _seedDate },
                new Product { Id = 5, Title = "Jeans", Description = "Blue Denim Jeans", Price = 800, Count = 12, CategoryId = 2, CreatedAt = _seedDate },
                new Product { Id = 6, Title = "Jacket", Description = "Winter Jacket", Price = 1500, Count = 7, CategoryId = 2, CreatedAt = _seedDate },
                new Product { Id = 7, Title = "C# Book", Description = "Learn C# Programming", Price = 450, Count = 25, CategoryId = 3, CreatedAt = _seedDate },
                new Product { Id = 8, Title = "ASP.NET Core Book", Description = "Master ASP.NET Core", Price = 550, Count = 18, CategoryId = 3, CreatedAt = _seedDate },
                new Product { Id = 9, Title = "Microwave", Description = "800W Microwave Oven", Price = 3200, Count = 6, CategoryId = 4, CreatedAt = _seedDate },
                new Product { Id = 10, Title = "Refrigerator", Description = "Double Door Fridge", Price = 12000, Count = 4, CategoryId = 4, CreatedAt = _seedDate },
                new Product { Id = 11, Title = "Football", Description = "Professional Football", Price = 250, Count = 30, CategoryId = 5, CreatedAt = _seedDate },
                new Product { Id = 12, Title = "Tennis Racket", Description = "Carbon Fiber Racket", Price = 1100, Count = 9, CategoryId = 5, CreatedAt = _seedDate },
                new Product { Id = 13, Title = "Watch", Description = "Digital Wrist Watch", Price = 600, Count = 14, CategoryId = 6, CreatedAt = _seedDate },
                new Product { Id = 14, Title = "Backpack", Description = "Laptop Backpack", Price = 700, Count = 16, CategoryId = 6, CreatedAt = _seedDate }
            );

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<Category> Categories => Set<Category>();
    }
}