using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class ShoppingCart
    {
        public Book book { get; set; }
        public int Count { get; set; }
    }
}
