using App_Dev.Models;

namespace App_Dev.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        public Order Order { get; set; }
    }
}
