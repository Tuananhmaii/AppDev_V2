using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace App_Dev.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Status { get; set; }
        [Required]
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
