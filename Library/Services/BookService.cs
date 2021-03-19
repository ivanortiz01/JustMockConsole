using Library.Models;
using Library.Repositories.Intefaces;

namespace Library.Services
{
    public class BookService
    {
        private IBookRepository repository;

        public BookService(IBookRepository repository)
        {
            this.repository = repository;
        }

        public Book GetSingleBook(int id)
        {
            return repository.GetWhere(book => book.Id == id);
        }
    }
}
