using Microsoft.EntityFrameworkCore;

namespace CompanySystem.DAL
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderWithItemsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }
    }
}
