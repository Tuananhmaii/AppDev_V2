using App_Dev.Data;
using App_Dev.Models;
using App_Dev.Repository.IRepository;

namespace App_Dev.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Book obj)
        {
            _db.Books.Update(obj); 
        }
    }
}
