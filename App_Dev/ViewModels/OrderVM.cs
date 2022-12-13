using App_Dev.Models;

namespace App_Dev.ViewModels
{
    public class OrderVM
    {
        public Order Order { get; set; }
        public IEnumerable<OrderItem> OrderItem { get; set; }
    }
}
