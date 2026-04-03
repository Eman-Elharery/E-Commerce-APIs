namespace CompanySystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartRepository CartRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public UnitOfWork(
            AppDbContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            ICartRepository cartRepository,
            IOrderRepository orderRepository)
        {
            _context = context;
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            CartRepository = cartRepository;
            OrderRepository = orderRepository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}