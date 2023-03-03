using Books.API.Entities;

namespace Books.API.Interfaces.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetBooks();
        Task<IEnumerable<Book>> GetBooksAsync();
        Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> ids);
        IAsyncEnumerable<Book> GetBooksAsAsyncEnumerable();
        Task<Book?> GetBookAsync(Guid id);
        void AddBook(Book book);
        Task<bool> SaveChangesAsync();
    }
}
