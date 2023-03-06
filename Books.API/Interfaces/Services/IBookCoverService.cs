using Books.API.Models.External;

namespace Books.API.Interfaces.Services
{
    public interface IBookCoverService
    {
        Task<BookCover?> GetBookCoverAsync(Guid id);
        Task<IEnumerable<BookCover>> GetBookCoversProcessOneByOneAsync(
            Guid id, CancellationToken token);
        Task<IEnumerable<BookCover>> GetBookCoversProcessAfterWaitForAllAsync(Guid id);
    }
}
