using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime DayCreated { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }
        [Required]
        public double TotalPrice { get; set; }




    }
}
