using Microsoft.EntityFrameworkCore;

namespace CompanySystem.DAL
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context) { }

        public async Task<Cart?> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task ClearCartAsync(string userId)
        {
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.Items);
                await _context.SaveChangesAsync();
            }
        }
    }
}
