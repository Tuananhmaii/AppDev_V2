namespace App_Dev.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IBookRepository Book { get; }
        IOrderItemRepository OrderItem { get; }
        IOrderRepository Order { get; }
        IShoppingCartRepository ShoppingCart { get; }
        void Save();
    }
}
