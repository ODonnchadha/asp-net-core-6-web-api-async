using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookResultFilter : IAsyncResultFilter
    {
        private readonly IMapper mapper;
        public BookResultFilter(IMapper mapper) => this.mapper = mapper;
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

            result.Value = mapper.Map<Models.Book>(result.Value);
            await next();
        }
    }
}
