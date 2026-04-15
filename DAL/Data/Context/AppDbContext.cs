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

            /*------ Roles ------*/
            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Id = "role-admin-guid-0001", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "role-admin-stamp-0001", Description = "Full system access" },
                new ApplicationRole { Id = "role-user-guid-0002", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "role-user-stamp-0002", Description = "Read-only access" }
            );

            /*------ Admin user ------*/
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
                new IdentityUserRole<string> { UserId = adminId, RoleId = "role-admin-guid-0001" }
            );

            /*------ Categories ------*/
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Fresh Fruits", Description = "Seasonal fresh fruits full of natural sweetness and vitamins.", ImageURL = "https://images.unsplash.com/photo-1610832958506-aa56368176cf?w=400&h=300&fit=crop", Slug = "fresh-fruits", CreatedAt = _seedDate },
                new Category { Id = 2, Name = "Vegetables", Description = "Farm-fresh vegetables for healthy everyday meals.", ImageURL = "https://images.unsplash.com/photo-1542838132-92c53300491e?w=400&h=300&fit=crop", Slug = "vegetables", CreatedAt = _seedDate },
                new Category { Id = 3, Name = "Herbs & Green", Description = "Fresh aromatic herbs and leafy greens for everyday cooking.", ImageURL = "https://planningagarden.com/wp-content/uploads/2023/02/image-3.jpeg", Slug = "herbs-greens", CreatedAt = _seedDate },
                new Category { Id = 4, Name = "Dried Fruits", Description = "Premium quality dried fruits full of natural sweetness.", ImageURL = "https://d2jx2rerrg6sh3.cloudfront.net/images/news/ImageForNews_785204_17210137222997757.jpg", Slug = "dried-fruits", CreatedAt = _seedDate },
                new Category { Id = 5, Name = "Dried Vegetables", Description = "Sun-dried vegetables for long-lasting freshness and flavor.", ImageURL = "https://tse4.mm.bing.net/th/id/OIP.h-BRxUaPTE40ulRlQ1PlUQHaE8?w=900&h=600&rs=1&pid=ImgDetMain&o=7&rm=3", Slug = "dried-vegetables", CreatedAt = _seedDate },
                new Category { Id = 6, Name = "Nuts", Description = "Premium nuts and seeds for healthy snacking.", ImageURL = "https://img.freepik.com/premium-photo/top-view-composition-assorted-nuts-creating-natural-background-with-various-types-nuts_174533-37574.jpg?w=2000", Slug = "nuts", CreatedAt = _seedDate },
                new Category { Id = 7, Name = "Ready to Cook", Description = "Pre-prepared ingredients for quick and easy meals.", ImageURL = "https://images.unsplash.com/photo-1547592180-85f173990554?w=400&h=300&fit=crop", Slug = "ready-to-cook", CreatedAt = _seedDate },
                new Category { Id = 8, Name = "Ready-to-use Vegetables", Description = "Pre-cut and washed vegetables ready to use instantly.", ImageURL = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=400&h=300&fit=crop", Slug = "ready-to-use-vegetables", CreatedAt = _seedDate }
            );

            /*------ Products ------*/
            modelBuilder.Entity<Product>().HasData(
                // Fresh Fruits
                new Product { Id = 101, Title = "Red Apple", Description = "Crisp and juicy red apples, full of natural sweetness and vitamins.", Price = 3.49m, Count = 150, Unit = "kg", Rating = 4.7, Reviews = 120, IsOrganic = true, IsFeatured = false, CategoryId = 1, ImageURL = "https://images.unsplash.com/photo-1567306226416-28f0efdc88ce?w=400&h=400&fit=crop", CreatedAt = _seedDate },
                new Product { Id = 102, Title = "Banana", Description = "Ripe yellow bananas, naturally sweet and packed with potassium.", Price = 2.49m, Count = 200, Unit = "bunch", Rating = 4.6, Reviews = 89, IsOrganic = false, IsFeatured = false, CategoryId = 1, ImageURL = "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=400&h=400&fit=crop", CreatedAt = _seedDate },
                new Product { Id = 103, Title = "Strawberries", Description = "Sweet and ripe strawberries, perfect for desserts or smoothies.", Price = 5.99m, Count = 90, Unit = "kg", Rating = 4.8, Reviews = 95, IsOrganic = true, IsFeatured = true, CategoryId = 1, ImageURL = "https://diyhomedesignideas.com/photos/800-webp/e2fo8m6o1bxd5lpsfn0q.webp", CreatedAt = _seedDate },
                new Product { Id = 104, Title = "Green Grapes", Description = "Juicy green grapes, naturally sweet and perfect for snacking.", Price = 4.99m, Count = 120, Unit = "kg", Rating = 4.5, Reviews = 76, IsOrganic = false, IsFeatured = true, CategoryId = 1, ImageURL = "https://dpof9ma0om0c1.cloudfront.net/images/content-images/green-grape-landing/green-grape-header-2.jpg", CreatedAt = _seedDate },
                new Product { Id = 105, Title = "Pineapple", Description = "Tropical pineapple, sweet, juicy and rich in vitamin C.", Price = 6.49m, Count = 50, Unit = "piece", Rating = 4.7, Reviews = 65, IsOrganic = true, IsFeatured = false, CategoryId = 1, ImageURL = "https://tse4.mm.bing.net/th/id/OIP.8Oc8ua0K7P84PpoJCP4JkQHaFj?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                // Vegetables
                new Product { Id = 201, Title = "Carrots", Description = "Fresh farm-grown carrots, crunchy and full of beta-carotene.", Price = 1.99m, Count = 180, Unit = "kg", Rating = 4.5, Reviews = 75, IsOrganic = true, IsFeatured = false, CategoryId = 2, ImageURL = "https://tse2.mm.bing.net/th/id/OIP.EOafQATWNirsC4jeS2vPHgHaFj?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 202, Title = "Broccoli", Description = "Fresh green broccoli, perfect for steaming, stir-fry, or salads.", Price = 3.29m, Count = 100, Unit = "kg", Rating = 4.6, Reviews = 88, IsOrganic = true, IsFeatured = false, CategoryId = 2, ImageURL = "https://tse3.mm.bing.net/th/id/OIP.NN02CpCK__5Di9H_CJS_PgHaFj?w=1200&h=900&rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 203, Title = "Tomatoes", Description = "Ripe and juicy tomatoes, perfect for salads, sauces, and cooking.", Price = 2.79m, Count = 140, Unit = "kg", Rating = 4.7, Reviews = 110, IsOrganic = false, IsFeatured = true, CategoryId = 2, ImageURL = "https://th.bing.com/th/id/R.5c476c3ea45bcf53cabd9d13aaae0b1f?rik=VNyQZ4Mhh%2bP8bg&riu=http%3a%2f%2fwww.primallyinspired.com%2fwp-content%2fuploads%2f2013%2f07%2ftomatoes.jpg&ehk=rGYZXIgLoY8HrJ1%2bPlt4DNyTW9CTf%2ftNIJ7LC0TFBFY%3d&risl=&pid=ImgRaw&r=0", CreatedAt = _seedDate },
                new Product { Id = 204, Title = "Cucumbers", Description = "Fresh and crunchy cucumbers, perfect for salads and snacking.", Price = 1.89m, Count = 130, Unit = "kg", Rating = 4.4, Reviews = 64, IsOrganic = false, IsFeatured = false, CategoryId = 2, ImageURL = "https://tse1.mm.bing.net/th/id/OIP.S0XqwAyhE10j_4S4wAiMdAHaHa?w=600&h=600&rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 205, Title = "Bell Peppers", Description = "Colorful bell peppers, sweet, crunchy, and full of vitamin C.", Price = 3.99m, Count = 80, Unit = "kg", Rating = 4.8, Reviews = 90, IsOrganic = true, IsFeatured = true, CategoryId = 2, ImageURL = "https://png.pngtree.com/thumb_back/fw800/background/20221014/pngtree-bell-peppers-stem-yellow-bell-pepper-photo-image_7085716.jpg", CreatedAt = _seedDate },
                // Herbs & Green
                new Product { Id = 301, Title = "Fresh Basil", Description = "Aromatic fresh basil leaves, perfect for Italian dishes and pesto.", Price = 2.99m, Count = 100, Unit = "bunch", Rating = 4.8, Reviews = 89, IsOrganic = true, IsFeatured = false, CategoryId = 3, ImageURL = "https://static.africaimages.com/photos/g/z/gzo9ijcch7FQo12xKmE3bquIX/gzo9ijcch7FQo12xKmE3bquIX_normal.jpg", CreatedAt = _seedDate },
                new Product { Id = 302, Title = "Spinach", Description = "Fresh leafy spinach, perfect for salads, soups, and smoothies.", Price = 2.29m, Count = 120, Unit = "bunch", Rating = 4.8, Reviews = 98, IsOrganic = true, IsFeatured = true, CategoryId = 3, ImageURL = "https://tse1.mm.bing.net/th/id/OIP.LOtuDfEdBmvLw1fu6O5qKwHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 303, Title = "Parsley", Description = "Fresh parsley, perfect for garnishing and adding flavor to any dish.", Price = 1.99m, Count = 90, Unit = "bunch", Rating = 4.7, Reviews = 70, IsOrganic = false, IsFeatured = false, CategoryId = 3, ImageURL = "https://www.goodtastingmeals.com/wp-content/uploads/2021/10/what-does-parsley-taste-like.jpg", CreatedAt = _seedDate },
                new Product { Id = 304, Title = "Cilantro", Description = "Fresh cilantro, great for salads, salsas, and soups.", Price = 2.19m, Count = 85, Unit = "bunch", Rating = 4.6, Reviews = 65, IsOrganic = true, IsFeatured = false, CategoryId = 3, ImageURL = "https://cdn11.bigcommerce.com/s-jmzfi5zcr2/products/1009/images/16162/Santo_Cilantro_Plant_Leaves__77445.1644445129.500.750.jpg?c=2", CreatedAt = _seedDate },
                // Dried Fruits
                new Product { Id = 401, Title = "Medjool Dates", Description = "Premium Medjool dates, naturally sweet and packed with fiber.", Price = 12.99m, Count = 60, Unit = "500g", Rating = 4.9, Reviews = 234, IsOrganic = true, IsFeatured = false, CategoryId = 4, ImageURL = "https://th.bing.com/th/id/R.cb34397905d881db71e5efda819bd95a?rik=XraiGTjqYttXxg&riu=http%3a%2f%2fshop.goodhillfarms.com%2fcdn%2fshop%2ffiles%2fmedjool-dates-good-hill-farms-52898976530802.jpg%3fv%3d1742750784&ehk=qwmBPAexReYtZGyjmT0a5ZlwvRlaySAlyZJkpN0rlKw%3d&risl=&pid=ImgRaw&r=0", CreatedAt = _seedDate },
                new Product { Id = 402, Title = "Dried Figs", Description = "Soft and sweet dried figs, great for snacking and baking.", Price = 10.49m, Count = 80, Unit = "500g", Rating = 4.7, Reviews = 145, IsOrganic = true, IsFeatured = false, CategoryId = 4, ImageURL = "https://tse3.mm.bing.net/th/id/OIP.hT1lbtyS9Pr-RYYnuOpC7wHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 403, Title = "Raisins", Description = "Sweet dried raisins, perfect for cereals, baking, or snacking.", Price = 6.99m, Count = 120, Unit = "500g", Rating = 4.6, Reviews = 90, IsOrganic = false, IsFeatured = true, CategoryId = 4, ImageURL = "https://th.bing.com/th/id/R.0e12c5914bc7ddf8d9ca1f9eff463f91?rik=%2bqZYk%2fisddFX4w&pid=ImgRaw&r=0", CreatedAt = _seedDate },
                // Dried Vegetables
                new Product { Id = 501, Title = "Sun-Dried Tomatoes", Description = "Intense flavor sun-dried tomatoes, perfect for pasta and sauces.", Price = 8.99m, Count = 50, Unit = "250g", Rating = 4.5, Reviews = 70, IsOrganic = true, IsFeatured = false, CategoryId = 5, ImageURL = "https://veganwithgusto.com/wp-content/uploads/2022/08/sun-dried-tomatoes-in-bowl.jpg", CreatedAt = _seedDate },
                new Product { Id = 502, Title = "Dried Bell Peppers", Description = "Crunchy dried bell peppers, great for soups, stews, and sauces.", Price = 7.49m, Count = 40, Unit = "250g", Rating = 4.4, Reviews = 50, IsOrganic = false, IsFeatured = false, CategoryId = 5, ImageURL = "https://m.media-amazon.com/images/I/71lcWW2OoIL._SL1014_.jpg", CreatedAt = _seedDate },
                // Nuts
                new Product { Id = 601, Title = "Raw Almonds", Description = "Premium raw almonds, perfect for snacking and baking.", Price = 14.99m, Count = 115, Unit = "500g", Rating = 4.9, Reviews = 267, IsOrganic = true, IsFeatured = false, CategoryId = 6, ImageURL = "https://images.unsplash.com/photo-1508061253366-f7da158b6d46?w=400&h=400&fit=crop", CreatedAt = _seedDate },
                new Product { Id = 602, Title = "Cashews", Description = "Delicious cashews, perfect for snacking or cooking.", Price = 16.49m, Count = 100, Unit = "500g", Rating = 4.8, Reviews = 200, IsOrganic = false, IsFeatured = false, CategoryId = 6, ImageURL = "https://tse3.mm.bing.net/th/id/OIP.99BTuSASnXhpplED3C09iwHaEJ?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 603, Title = "Walnuts", Description = "Fresh and crunchy walnuts, ideal for baking and healthy snacks.", Price = 18.99m, Count = 80, Unit = "500g", Rating = 4.7, Reviews = 150, IsOrganic = true, IsFeatured = true, CategoryId = 6, ImageURL = "https://tse1.mm.bing.net/th/id/OIP.xHBI6PUlSitRSmQk1mobzgHaEJ?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                // Ready to Cook
                new Product { Id = 701, Title = "Vegetable Stir-Fry Mix", Description = "Pre-cut vegetables ready for a quick and healthy stir-fry meal.", Price = 5.99m, Count = 60, Unit = "pack", Rating = 4.6, Reviews = 55, IsOrganic = false, IsFeatured = false, CategoryId = 7, ImageURL = "https://tse4.mm.bing.net/th/id/OIP._dpP6W5MUeTHtp6wXaabYAHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                new Product { Id = 702, Title = "Chicken Curry Kit", Description = "All ingredients pre-measured and ready for a quick chicken curry.", Price = 9.99m, Count = 40, Unit = "kit", Rating = 4.7, Reviews = 60, IsOrganic = true, IsFeatured = false, CategoryId = 7, ImageURL = "https://tse2.mm.bing.net/th/id/OIP.kEWDhQr7To2BE6NZtYyQEgHaLH?rs=1&pid=ImgDetMain&o=7&rm=3", CreatedAt = _seedDate },
                // Ready-to-use Vegetables
                new Product { Id = 801, Title = "Pre-Cut Mixed Veggies", Description = "A mix of pre-cut and washed vegetables ready to use instantly.", Price = 4.99m, Count = 80, Unit = "pack", Rating = 4.3, Reviews = 45, IsOrganic = false, IsFeatured = true, CategoryId = 8, ImageURL = "https://img.freepik.com/premium-photo/variety-chopped-vegetables-ready-stew-created-with-generative-ai_419341-56888.jpg", CreatedAt = _seedDate },
                new Product { Id = 802, Title = "Pre-Cut Carrots", Description = "Freshly washed and cut carrots, ready to add to any dish.", Price = 3.49m, Count = 60, Unit = "pack", Rating = 4.5, Reviews = 40, IsOrganic = true, IsFeatured = false, CategoryId = 8, ImageURL = "https://img.freepik.com/premium-photo/background-features-sliced-carrots_961875-393364.jpg", CreatedAt = _seedDate }
            );

            /*------ Cart config ------*/
            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.UserId).IsUnique();
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User).WithMany()
                .HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => new { ci.CartId, ci.ProductId }).IsUnique();
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Cart).WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product).WithMany()
                .HasForeignKey(ci => ci.ProductId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.UnitPrice).HasColumnType("decimal(18,2)");

            /*------ Order config ------*/
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User).WithMany()
                .HasForeignKey(o => o.UserId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order).WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product).WithMany()
                .HasForeignKey(oi => oi.ProductId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice).HasColumnType("decimal(18,2)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public virtual DbSet<Product> Products => Set<Product>();
        public virtual DbSet<Category> Categories => Set<Category>();
        public virtual DbSet<Cart> Carts => Set<Cart>();
        public virtual DbSet<CartItem> CartItems => Set<CartItem>();
        public virtual DbSet<Order> Orders => Set<Order>();
        public virtual DbSet<OrderItem> OrderItems => Set<OrderItem>();
    }
}