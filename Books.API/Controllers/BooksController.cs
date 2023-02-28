using Books.API.Filters;
using Books.API.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController(), Route("api")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository repository;
        public BooksController(IBookRepository repository) => this.repository = repository;

        [HttpGet("books")]
        [TypeFilter(typeof(BooksResultFilter))]
        public async Task<IActionResult> GetBooks()
        {
            var entities = await repository.GetBooksAsync();

            return Ok(entities);
        }

        [HttpGet("books/{id}")]
        [TypeFilter(typeof(BookResultFilter))]
        public async Task<IActionResult> GetBook(Guid id)
        {
            var entity = await repository.GetBookAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
