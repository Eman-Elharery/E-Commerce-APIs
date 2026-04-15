using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Slug = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShippingPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Count = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Reviews = table.Column<int>(type: "int", nullable: false),
                    IsOrganic = table.Column<bool>(type: "bit", nullable: false),
                    IsFeatured = table.Column<bool>(type: "bit", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "role-admin-guid-0001", "role-admin-stamp-0001", "Full system access", "Admin", "ADMIN" },
                    { "role-user-guid-0002", "role-user-stamp-0002", "Read-only access", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[] { "user-admin-guid-0001", 0, "admin-concurrency-stamp-fixed", new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "admin@company.com", true, "System", "Admin", false, null, "ADMIN@COMPANY.COM", "ADMIN", "AQAAAAIAAYagAAAAEDkIZ0tcFEeu7jIpN95iNUAh2tk+s8EaF81lOQZJk24GQpewc8gAAyJRo/hG9mp/DA==", null, false, "admin-security-stamp-fixed", false, null, "admin" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Description", "ImageURL", "Name", "Slug", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Seasonal fresh fruits full of natural sweetness and vitamins.", "https://images.unsplash.com/photo-1610832958506-aa56368176cf?w=400&h=300&fit=crop", "Fresh Fruits", "fresh-fruits", null },
                    { 2, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Farm-fresh vegetables for healthy everyday meals.", "https://images.unsplash.com/photo-1542838132-92c53300491e?w=400&h=300&fit=crop", "Vegetables", "vegetables", null },
                    { 3, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh aromatic herbs and leafy greens for everyday cooking.", "https://planningagarden.com/wp-content/uploads/2023/02/image-3.jpeg", "Herbs & Green", "herbs-greens", null },
                    { 4, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Premium quality dried fruits full of natural sweetness.", "https://d2jx2rerrg6sh3.cloudfront.net/images/news/ImageForNews_785204_17210137222997757.jpg", "Dried Fruits", "dried-fruits", null },
                    { 5, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Sun-dried vegetables for long-lasting freshness and flavor.", "https://tse4.mm.bing.net/th/id/OIP.h-BRxUaPTE40ulRlQ1PlUQHaE8?w=900&h=600&rs=1&pid=ImgDetMain&o=7&rm=3", "Dried Vegetables", "dried-vegetables", null },
                    { 6, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Premium nuts and seeds for healthy snacking.", "https://img.freepik.com/premium-photo/top-view-composition-assorted-nuts-creating-natural-background-with-various-types-nuts_174533-37574.jpg?w=2000", "Nuts", "nuts", null },
                    { 7, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Pre-prepared ingredients for quick and easy meals.", "https://images.unsplash.com/photo-1547592180-85f173990554?w=400&h=300&fit=crop", "Ready to Cook", "ready-to-cook", null },
                    { 8, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Pre-cut and washed vegetables ready to use instantly.", "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?w=400&h=300&fit=crop", "Ready-to-use Vegetables", "ready-to-use-vegetables", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "role-admin-guid-0001", "user-admin-guid-0001" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Count", "CreatedAt", "Description", "ImageURL", "IsFeatured", "IsOrganic", "Price", "Rating", "Reviews", "Title", "Unit", "UpdatedAt" },
                values: new object[,]
                {
                    { 101, 1, 150, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Crisp and juicy red apples, full of natural sweetness and vitamins.", "https://images.unsplash.com/photo-1567306226416-28f0efdc88ce?w=400&h=400&fit=crop", false, true, 3.49m, 4.7000000000000002, 120, "Red Apple", "kg", null },
                    { 102, 1, 200, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Ripe yellow bananas, naturally sweet and packed with potassium.", "https://images.unsplash.com/photo-1571771894821-ce9b6c11b08e?w=400&h=400&fit=crop", false, false, 2.49m, 4.5999999999999996, 89, "Banana", "bunch", null },
                    { 103, 1, 90, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Sweet and ripe strawberries, perfect for desserts or smoothies.", "https://diyhomedesignideas.com/photos/800-webp/e2fo8m6o1bxd5lpsfn0q.webp", true, true, 5.99m, 4.7999999999999998, 95, "Strawberries", "kg", null },
                    { 104, 1, 120, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Juicy green grapes, naturally sweet and perfect for snacking.", "https://dpof9ma0om0c1.cloudfront.net/images/content-images/green-grape-landing/green-grape-header-2.jpg", true, false, 4.99m, 4.5, 76, "Green Grapes", "kg", null },
                    { 105, 1, 50, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Tropical pineapple, sweet, juicy and rich in vitamin C.", "https://tse4.mm.bing.net/th/id/OIP.8Oc8ua0K7P84PpoJCP4JkQHaFj?rs=1&pid=ImgDetMain&o=7&rm=3", false, true, 6.49m, 4.7000000000000002, 65, "Pineapple", "piece", null },
                    { 201, 2, 180, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh farm-grown carrots, crunchy and full of beta-carotene.", "https://tse2.mm.bing.net/th/id/OIP.EOafQATWNirsC4jeS2vPHgHaFj?rs=1&pid=ImgDetMain&o=7&rm=3", false, true, 1.99m, 4.5, 75, "Carrots", "kg", null },
                    { 202, 2, 100, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh green broccoli, perfect for steaming, stir-fry, or salads.", "https://tse3.mm.bing.net/th/id/OIP.NN02CpCK__5Di9H_CJS_PgHaFj?w=1200&h=900&rs=1&pid=ImgDetMain&o=7&rm=3", false, true, 3.29m, 4.5999999999999996, 88, "Broccoli", "kg", null },
                    { 203, 2, 140, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Ripe and juicy tomatoes, perfect for salads, sauces, and cooking.", "https://th.bing.com/th/id/R.5c476c3ea45bcf53cabd9d13aaae0b1f?rik=VNyQZ4Mhh%2bP8bg&riu=http%3a%2f%2fwww.primallyinspired.com%2fwp-content%2fuploads%2f2013%2f07%2ftomatoes.jpg&ehk=rGYZXIgLoY8HrJ1%2bPlt4DNyTW9CTf%2ftNIJ7LC0TFBFY%3d&risl=&pid=ImgRaw&r=0", true, false, 2.79m, 4.7000000000000002, 110, "Tomatoes", "kg", null },
                    { 204, 2, 130, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh and crunchy cucumbers, perfect for salads and snacking.", "https://tse1.mm.bing.net/th/id/OIP.S0XqwAyhE10j_4S4wAiMdAHaHa?w=600&h=600&rs=1&pid=ImgDetMain&o=7&rm=3", false, false, 1.89m, 4.4000000000000004, 64, "Cucumbers", "kg", null },
                    { 205, 2, 80, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Colorful bell peppers, sweet, crunchy, and full of vitamin C.", "https://png.pngtree.com/thumb_back/fw800/background/20221014/pngtree-bell-peppers-stem-yellow-bell-pepper-photo-image_7085716.jpg", true, true, 3.99m, 4.7999999999999998, 90, "Bell Peppers", "kg", null },
                    { 301, 3, 100, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Aromatic fresh basil leaves, perfect for Italian dishes and pesto.", "https://static.africaimages.com/photos/g/z/gzo9ijcch7FQo12xKmE3bquIX/gzo9ijcch7FQo12xKmE3bquIX_normal.jpg", false, true, 2.99m, 4.7999999999999998, 89, "Fresh Basil", "bunch", null },
                    { 302, 3, 120, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh leafy spinach, perfect for salads, soups, and smoothies.", "https://tse1.mm.bing.net/th/id/OIP.LOtuDfEdBmvLw1fu6O5qKwHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", true, true, 2.29m, 4.7999999999999998, 98, "Spinach", "bunch", null },
                    { 303, 3, 90, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh parsley, perfect for garnishing and adding flavor to any dish.", "https://www.goodtastingmeals.com/wp-content/uploads/2021/10/what-does-parsley-taste-like.jpg", false, false, 1.99m, 4.7000000000000002, 70, "Parsley", "bunch", null },
                    { 304, 3, 85, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh cilantro, great for salads, salsas, and soups.", "https://cdn11.bigcommerce.com/s-jmzfi5zcr2/products/1009/images/16162/Santo_Cilantro_Plant_Leaves__77445.1644445129.500.750.jpg?c=2", false, true, 2.19m, 4.5999999999999996, 65, "Cilantro", "bunch", null },
                    { 401, 4, 60, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Premium Medjool dates, naturally sweet and packed with fiber.", "https://th.bing.com/th/id/R.cb34397905d881db71e5efda819bd95a?rik=XraiGTjqYttXxg&riu=http%3a%2f%2fshop.goodhillfarms.com%2fcdn%2fshop%2ffiles%2fmedjool-dates-good-hill-farms-52898976530802.jpg%3fv%3d1742750784&ehk=qwmBPAexReYtZGyjmT0a5ZlwvRlaySAlyZJkpN0rlKw%3d&risl=&pid=ImgRaw&r=0", false, true, 12.99m, 4.9000000000000004, 234, "Medjool Dates", "500g", null },
                    { 402, 4, 80, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Soft and sweet dried figs, great for snacking and baking.", "https://tse3.mm.bing.net/th/id/OIP.hT1lbtyS9Pr-RYYnuOpC7wHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", false, true, 10.49m, 4.7000000000000002, 145, "Dried Figs", "500g", null },
                    { 403, 4, 120, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Sweet dried raisins, perfect for cereals, baking, or snacking.", "https://th.bing.com/th/id/R.0e12c5914bc7ddf8d9ca1f9eff463f91?rik=%2bqZYk%2fisddFX4w&pid=ImgRaw&r=0", true, false, 6.99m, 4.5999999999999996, 90, "Raisins", "500g", null },
                    { 501, 5, 50, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Intense flavor sun-dried tomatoes, perfect for pasta and sauces.", "https://veganwithgusto.com/wp-content/uploads/2022/08/sun-dried-tomatoes-in-bowl.jpg", false, true, 8.99m, 4.5, 70, "Sun-Dried Tomatoes", "250g", null },
                    { 502, 5, 40, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Crunchy dried bell peppers, great for soups, stews, and sauces.", "https://m.media-amazon.com/images/I/71lcWW2OoIL._SL1014_.jpg", false, false, 7.49m, 4.4000000000000004, 50, "Dried Bell Peppers", "250g", null },
                    { 601, 6, 115, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Premium raw almonds, perfect for snacking and baking.", "https://images.unsplash.com/photo-1508061253366-f7da158b6d46?w=400&h=400&fit=crop", false, true, 14.99m, 4.9000000000000004, 267, "Raw Almonds", "500g", null },
                    { 602, 6, 100, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Delicious cashews, perfect for snacking or cooking.", "https://tse3.mm.bing.net/th/id/OIP.99BTuSASnXhpplED3C09iwHaEJ?rs=1&pid=ImgDetMain&o=7&rm=3", false, false, 16.49m, 4.7999999999999998, 200, "Cashews", "500g", null },
                    { 603, 6, 80, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Fresh and crunchy walnuts, ideal for baking and healthy snacks.", "https://tse1.mm.bing.net/th/id/OIP.xHBI6PUlSitRSmQk1mobzgHaEJ?rs=1&pid=ImgDetMain&o=7&rm=3", true, true, 18.99m, 4.7000000000000002, 150, "Walnuts", "500g", null },
                    { 701, 7, 60, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Pre-cut vegetables ready for a quick and healthy stir-fry meal.", "https://tse4.mm.bing.net/th/id/OIP._dpP6W5MUeTHtp6wXaabYAHaHa?rs=1&pid=ImgDetMain&o=7&rm=3", false, false, 5.99m, 4.5999999999999996, 55, "Vegetable Stir-Fry Mix", "pack", null },
                    { 702, 7, 40, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "All ingredients pre-measured and ready for a quick chicken curry.", "https://tse2.mm.bing.net/th/id/OIP.kEWDhQr7To2BE6NZtYyQEgHaLH?rs=1&pid=ImgDetMain&o=7&rm=3", false, true, 9.99m, 4.7000000000000002, 60, "Chicken Curry Kit", "kit", null },
                    { 801, 8, 80, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "A mix of pre-cut and washed vegetables ready to use instantly.", "https://img.freepik.com/premium-photo/variety-chopped-vegetables-ready-stew-created-with-generative-ai_419341-56888.jpg", true, false, 4.99m, 4.2999999999999998, 45, "Pre-Cut Mixed Veggies", "pack", null },
                    { 802, 8, 60, new DateTime(2026, 3, 1, 10, 30, 0, 0, DateTimeKind.Unspecified), "Freshly washed and cut carrots, ready to add to any dish.", "https://img.freepik.com/premium-photo/background-features-sliced-carrots_961875-393364.jpg", false, true, 3.49m, 4.5, 40, "Pre-Cut Carrots", "pack", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId_ProductId",
                table: "CartItems",
                columns: new[] { "CartId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
