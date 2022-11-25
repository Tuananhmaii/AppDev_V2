using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool IsOrder { get; set; } = false;
    }
}
