using Books.API.Entities;

namespace Books.API.Interfaces.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<Book?> GetBookAsync(Guid id);
    }
}
