using App_Dev.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App_Dev.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get;set; }
        public DbSet<Category> Categories { get;set; }
        public DbSet<Book> Books { get;set; }
        public DbSet<Request> Requests { get;set; }
        public DbSet<OrderItem> OrderItems { get;set; }
        public DbSet<Order> Orders { get;set; }
        public DbSet<ShoppingCart> ShoppingCarts { get;set; }
    }
}