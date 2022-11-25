using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public bool IsApprove { get; set; } = false;
    }
}
