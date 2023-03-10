using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BooksResultFilter : IAsyncResultFilter
    {
        private readonly IMapper mapper;

        public BooksResultFilter(IMapper mapper) => this.mapper = mapper;
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

            result.Value = mapper.Map<IEnumerable<Models.Book>>(result.Value);
            await next();
        }
    }
}
