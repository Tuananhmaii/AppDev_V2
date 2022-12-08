using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App_Dev.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int BookID { get; set; }
        [ValidateNever]
        public Book Book { get; set; }
        public string ApplicationUserId { get; set; }
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
    }
}
