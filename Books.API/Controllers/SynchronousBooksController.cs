using Books.API.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController(), Route("api")]
    public class SynchronousBooksController : ControllerBase
    {
        private readonly IBookRepository repository;
        public SynchronousBooksController(IBookRepository repository) => this.repository = repository;

        [HttpGet("books/sync")]
        public IActionResult GetBooks()
        {
            var entities = repository.GetBooks();

            return Ok(entities);
        }
    }
}
