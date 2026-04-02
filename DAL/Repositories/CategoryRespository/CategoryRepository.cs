using Microsoft.EntityFrameworkCore;

namespace CompanySystem.DAL
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Category>> GetAllWithProductsAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }
    }
}