using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ValidateNever]
        public Order Order { get; set; }
        [Required]
        public int BookId { get; set; }
        [ValidateNever]
        public Book Book { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
