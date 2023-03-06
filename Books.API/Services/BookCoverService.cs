using Books.API.Interfaces.Services;
using Books.API.Models.External;
using System;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Books.API.Services
{
    public class BookCoverService : IBookCoverService
    {
        private const string URL_ROOT = "http://localhost:5135";
        private readonly IHttpClientFactory factory;

        public BookCoverService(IHttpClientFactory factory) => this.factory = factory;
        public async Task<BookCover?> GetBookCoverAsync(Guid id)
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync(
                $"{URL_ROOT}/api/bookcovers/{id}");

            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<BookCover>(
                    await response.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return null;
        }

        public async Task<IEnumerable<BookCover>> GetBookCoversProcessAfterWaitForAllAsync(Guid id)
        {
            var client = factory.CreateClient();

            var covers = new List<BookCover>();

            var urls = new[]
            {
                $"{URL_ROOT}/api/bookcovers/{id}-aaa",
                $"{URL_ROOT}/api/bookcovers/{id}-bbb",
                $"{URL_ROOT}/api/bookcovers/{id}-ccc",
                $"{URL_ROOT}/api/bookcovers/{id}-ddd",
                $"{URL_ROOT}/api/bookcovers/{id}-eee",
                $"{URL_ROOT}/api/bookcovers/{id}-fff"
            };

            var tsks = new List<Task<HttpResponseMessage>>();
            foreach (var url in urls)
            {
                tsks.Add(client.GetAsync(url));
            }

            // NOTE: WhenAny() allows for continuation when 1:N task(s) complete.
            var results = await Task.WhenAll(tsks);

            foreach (var result in results.Reverse()) // e.g.:
            {
                var c = JsonSerializer.Deserialize<BookCover>(
                    await result.Content.ReadAsStringAsync(),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (null != c)
                {
                    covers.Add(c);
                }
            }

            return covers;
        }

        public async Task<IEnumerable<BookCover>> GetBookCoversProcessOneByOneAsync(
            Guid id, CancellationToken token)
        {
            var client = factory.CreateClient();
            var covers = new List<BookCover>();
            var urls = new[]
            {
                $"{URL_ROOT}/api/bookcovers/{id}-aaa",
                $"{URL_ROOT}/api/bookcovers/{id}-bbb?returnFault=true",
                $"{URL_ROOT}/api/bookcovers/{id}-ccc",
                $"{URL_ROOT}/api/bookcovers/{id}-ddd",
                $"{URL_ROOT}/api/bookcovers/{id}-eee",
                $"{URL_ROOT}/api/bookcovers/{id}-fff"
            };

            using (var cancel = new CancellationTokenSource())
            {
                using (var link = CancellationTokenSource.CreateLinkedTokenSource(cancel.Token, token))
                {
                    foreach (var url in urls)
                    {
                        var response = await client.GetAsync(url, link.Token);

                        if (response.IsSuccessStatusCode)
                        {
                            // await will pause execution. Urls will process one-by-one in sort order.
                            var c = JsonSerializer.Deserialize<BookCover>(
                                await response.Content.ReadAsStringAsync(link.Token),
                                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            if (null != c)
                            {
                                covers.Add(c);
                            }
                        }
                        else { cancel.Cancel(); }
                    }
                }
            }

            return covers;
        }
    }
}
