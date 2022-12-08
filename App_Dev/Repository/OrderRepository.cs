using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;

namespace App_Dev.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order obj)
        {
            _db.Orders.Update(obj);
        }
    }
}
