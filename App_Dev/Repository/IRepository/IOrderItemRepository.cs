using App_Dev.Models;

namespace App_Dev.Repository.IRepository
{
    public interface IOrderItemRepository : IRepository<OrderItem>
    {
        void Update(OrderItem obj);
    }
}
