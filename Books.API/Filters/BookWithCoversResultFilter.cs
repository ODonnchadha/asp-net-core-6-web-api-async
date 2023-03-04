using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Books.API.Models.External;
using Books.API.Models;

namespace Books.API.Filters
{
    public class BookWithCoversResultFilter : IAsyncResultFilter
    {
        private readonly IMapper mapper;
        public BookWithCoversResultFilter(IMapper mapper) => this.mapper = mapper;
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;

            if (result == null || result.Value == null)
            {
                await next();
                return;
            }
            if (result.StatusCode < 200 || result.StatusCode >= 300)
            {
                await next();
                return;
            }

            var (book, covers) = 
                ((Entities.Book book, IEnumerable<Models.External.BookCover> covers))result.Value;

            // Map multiple objects into one.
            var mappedBook = mapper.Map<BookWithCovers>(book);
            result.Value = mapper.Map(covers, mappedBook);

            await next();
        }
    }
}
