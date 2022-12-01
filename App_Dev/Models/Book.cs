using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Author { get; set; }
        public string? Image { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Description { get; set; }
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
