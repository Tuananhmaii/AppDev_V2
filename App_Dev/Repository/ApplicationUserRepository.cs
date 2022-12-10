using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;

namespace App_Dev.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
