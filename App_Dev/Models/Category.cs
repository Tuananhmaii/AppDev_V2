using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
