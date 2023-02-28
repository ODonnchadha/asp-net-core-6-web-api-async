using AutoMapper;
using Books.API.Extensions.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Books.API.Filters
{
    public class BookResultFilter : IAsyncResultFilter
    {
        private readonly IMapper mapper;

        //[HttpGet("books/{id}")]
        //[TypeFilter(typeof(BookResultFilterAttribute))]
        //public async Task<IActionResult> GetBook(Guid id)
        public BookResultFilter(IMapper mapper) => this.mapper = mapper;
        public async Task OnResultExecutionAsync(
            ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;

            if (!resultFromAction.IsValidRequest())
            {
                await next();
                return;
            }

            //[HttpGet("books/{id}")]
            //[BookResultFilter()]
            //public async Task<IActionResult> GetBook(Guid id)
            // var mapper = context.HttpContext.RequestServices.GetRequiredService<IMapper>();

            //if (typeof(IEnumerable<>).IsAssignableFrom(resultFromAction.GetType()))
            //{
            //    resultFromAction.Value = mapper.Map<IEnumerable<Models.Book>>(resultFromAction.Value);
            //}
            //else
            //{
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            resultFromAction.Value = mapper.Map<Models.Book>(resultFromAction.Value);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                              //}

            await next();
        }
    }
}
