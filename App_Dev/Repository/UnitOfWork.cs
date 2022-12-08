using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;

namespace App_Dev.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Book = new BookRepository(_db);
            OrderItem = new OrderItemRepository(_db);
            Order = new OrderRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public IBookRepository Book { get; private set; }
        public IOrderItemRepository OrderItem { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
