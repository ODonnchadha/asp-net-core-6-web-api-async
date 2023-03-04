using Microsoft.AspNetCore.Mvc;

namespace Books.BookCovers.API.Controllers
{
    [ApiController(), Route("api/bookcovers")]
    public class BookCoversController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookCover(string id, bool returnFault = false)
        {
            if (returnFault)
            {
                await Task.Delay(100);
                return new StatusCodeResult(500);
            }

            // Generate a "book cover" (byte array) between 5MB and 10MB.
            var r = new Random();
            int i = r.Next(5097152, 10485760);
            byte[] cover = new byte[i];
            r.NextBytes(cover);

            // SImply echo the supplied id straight through to the response
            return Ok(new { Id = id, Content = cover });
        }
    }
}
