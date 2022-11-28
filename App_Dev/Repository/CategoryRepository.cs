using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;

namespace App_Dev.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj); 
        }
    }
}
