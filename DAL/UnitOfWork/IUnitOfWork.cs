namespace CompanySystem.DAL
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICartRepository CartRepository { get; }
        public IOrderRepository OrderRepository { get; }
        void Save();
    }
}