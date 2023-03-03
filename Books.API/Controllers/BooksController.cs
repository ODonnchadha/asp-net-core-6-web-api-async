using AutoMapper;
using Books.API.Filters;
using Books.API.Interfaces.Repositories;
using Books.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController(), Route("api")]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;
        public BooksController(IBookRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpGet("books")]
        [TypeFilter(typeof(BooksResultFilter))]
        public async Task<IActionResult> GetBooks()
        {
            var entities = await repository.GetBooksAsync();

            return Ok(entities);
        }

        [HttpGet("stream")]
        public async IAsyncEnumerable<Book> StreamBooks()
        {
            await foreach(var b in repository.GetBooksAsAsyncEnumerable())
            {
                await Task.Delay(500);
                yield return mapper.Map<Book>(b);
            }
        }

        [HttpPost("books")]
        [TypeFilter(typeof(BooksResultFilter))]
        public async Task<IActionResult> CreateBook([FromBody] BookForCreation book)
        {
            var entity = mapper.Map<Entities.Book>(book);

            repository.AddBook(entity);
            await repository.SaveChangesAsync();

            // Never return an entity. Thus, the filter & the context method.
            await repository.GetBookAsync(entity.Id);
            return CreatedAtRoute("GET_BOOK", new { id = entity.Id }, entity);
        }

        [HttpGet("books/{id}",Name ="GET_BOOK")]
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
