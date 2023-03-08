using AutoMapper;
using Books.API.Filters;
using Books.API.Interfaces.Repositories;
using Books.API.Interfaces.Services;
using Books.API.Models;
using Books.Legacy;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Controllers
{
    [ApiController(), Route("api")]
    public class BooksController : ControllerBase
    {
        private readonly IBookCoverService service;
        private readonly IBookRepository repository;
        private readonly ILogger<BooksController> logger;
        private readonly IMapper mapper;
        public BooksController(
            IBookCoverService service, IBookRepository repository, ILogger<BooksController> logger, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.repository = repository;
            this.service = service;
        }

        [HttpGet("books")]
        [TypeFilter(typeof(BooksResultFilter))]
        public async Task<IActionResult> GetBooks()
        {
            var entities = await repository.GetBooksAsync();
            return Ok(entities);
        }

        //[HttpGet("books")]
        //[TypeFilter(typeof(BooksResultFilter))]
        //public IActionResult GetBooks_BadCode()
        //{
        //    // This will block threads.
        //    var entities = repository.GetBooksAsync().Result;
        //    return Ok(entities);
        //}

        [HttpGet("stream")]
        public async IAsyncEnumerable<Models.Book> StreamBooks()
        {
            await foreach(var b in repository.GetBooksAsAsyncEnumerable())
            {
                await Task.Delay(500);
                yield return mapper.Map<Models.Book>(b);
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
        [TypeFilter(typeof(BookWithCoversResultFilter))]
        public async Task<IActionResult> GetBook(Guid id, CancellationToken token)
        {
            logger.LogInformation(
                $"Thread when entering GetBook() {Thread.CurrentThread.ManagedThreadId}.");


            var entity = await repository.GetBookAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            // e.g.:
            var bookCover = await service.GetBookCoverAsync(id);

            // Using a CancellationTokenSource:
            var bookCoversProcessOneByOne = 
                await service.GetBookCoversProcessOneByOneAsync(id, token);

            var bookCoversProcessAfterWaitForAll = 
                await service.GetBookCoversProcessAfterWaitForAllAsync(id);

            logger.LogInformation(
                $"Thread when launching GetLegacyBookPages_BadCode() {Thread.CurrentThread.ManagedThreadId}.");
            var pages = await GetLegacyBookPages_BadCode(id);


            // e.g.:
            // if (token.IsCancellationRequested)
            // token.ThrowIfCancellationRequested();
            

            // A book with covers.
            // a.
            //var bag = new Tuple<Entities.Book, IEnumerable<BookCover>>(
            //    entity, bookCoversProcessAfterWaitForAll);
            //bag.Item1; bag.Item2;

            // b.
            //(Entities.Book book, IEnumerable<BookCover> covers) bag = 
            //    (entity, bookCoversProcessAfterWaitForAll);

            // c. NOTE: Passing multiple objects.
            // return Ok();
            return Ok((book: entity, covers: bookCoversProcessAfterWaitForAll));
        }

        private Task<int> GetLegacyBookPages_BadCode(Guid id)
        {
            return (Task<int>)Task.Run(() =>
            {
                var calc = new ComplicatedPageCalculator();

                logger.LogInformation(
                    $"Thread when running GetLegacyBookPages_BadCode() {Thread.CurrentThread.ManagedThreadId}.");

                var pages = calc.CalculateBookPages(id);
            });
        }
    }
}
