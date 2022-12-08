using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;

namespace App_Dev.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private ApplicationDbContext _db;
        public OrderItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderItem obj)
        {
            _db.OrderItems.Update(obj);
        }
    }
}
