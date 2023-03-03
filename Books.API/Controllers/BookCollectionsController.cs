using AutoMapper;
using Books.API.Filters;
using Books.API.Helpers;
using Books.API.Interfaces.Repositories;
using Books.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController(), Route("api/bookcollections"), TypeFilter(typeof(BooksResultFilter))]
    public class BookCollectionsController : ControllerBase
    {
        private readonly IBookRepository repository;
        private readonly IMapper mapper;
        public BookCollectionsController(IBookRepository repository, IMapper mapper)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        [HttpPost()]
        public async Task<IActionResult> CreateBookCollection(
            [FromBody] IEnumerable<BookForCreation> books)
        {
            var entities = mapper.Map<IEnumerable<Entities.Book>>(books);

            foreach (var entity in entities)
            {
                repository.AddBook(entity);
            }

            await repository.SaveChangesAsync();

            var booksToReturn = await repository.GetBooksAsync(
                entities.Select(e => e.Id).ToList());
            var bookIds = string.Join(",", booksToReturn.Select(b => b.Id));

            return CreatedAtRoute("GET_BOOKS", new { bookIds }, booksToReturn);
        }

        [HttpGet("({ids})", Name = "GET_BOOKS")]
        public async Task<IActionResult> GetBookCollection(
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            var entities = await repository.GetBooksAsync(ids);

            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }

            return Ok(entities);
        }
    }
}
