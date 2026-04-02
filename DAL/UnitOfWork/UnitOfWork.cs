namespace CompanySystem.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public UnitOfWork(AppDbContext context,
            IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            ProductRepository = productRepository;
            CategoryRepository = categoryRepository;
            _context = context;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
