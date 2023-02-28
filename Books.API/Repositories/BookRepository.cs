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
        public async Task<Book?> GetBookAsync(Guid id) =>
            await context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        public IEnumerable<Book> GetBooks() => context.Books.Include(b => b.Author).ToList();
        public async Task<IEnumerable<Book>> GetBooksAsync() =>
            await context.Books.Include(b => b.Author).ToListAsync();
    }
}
