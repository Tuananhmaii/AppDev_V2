using App_Dev.Models;

namespace App_Dev.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book obj);
    }
}
