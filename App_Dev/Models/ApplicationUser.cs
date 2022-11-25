using Microsoft.AspNetCore.Identity;

namespace App_Dev.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
    }
}
