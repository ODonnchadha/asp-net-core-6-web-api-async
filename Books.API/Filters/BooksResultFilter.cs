using AutoMapper;
using Books.API.Extensions.Filters;
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
            var resultFromAction = context.Result as ObjectResult;

            if (!resultFromAction.IsValidRequest())
            {
                await next();
                return;
            }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            resultFromAction.Value = 
                mapper.Map<IEnumerable<Models.Book>>(resultFromAction.Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            //}
            await next();
        }
    }
}
