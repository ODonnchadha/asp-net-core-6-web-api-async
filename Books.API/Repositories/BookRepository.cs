using Books.API.Contexts;
using Books.API.Entities;
using Books.API.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext context;
        public BookRepository(BookContext context) => this.context = context;
        public void AddBook(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            context.Books.Add(book);
        }
        public async Task<Book?> GetBookAsync(Guid id) =>
            await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        public IEnumerable<Book> GetBooks() => context.Books.Include(b => b.Author).ToList();
        public async Task<IEnumerable<Book>> GetBooksAsync() =>
            await context.Books.Include(b => b.Author).ToListAsync();
        public IAsyncEnumerable<Book> GetBooksAsAsyncEnumerable() => 
            context.Books.AsAsyncEnumerable<Book>();
        public async Task<IEnumerable<Book>> GetBooksAsync(IEnumerable<Guid> ids) =>
            await context.Books.Where(b => ids.Contains(b.Id)).ToListAsync();
        public async Task<bool> SaveChangesAsync() =>
            (await context.SaveChangesAsync() > 0);
    }
}
