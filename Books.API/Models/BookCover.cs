namespace Books.API.Models
{
    public class BookCover
    {
        public string Id { get; set; }
        public byte[]? Content { get; set; }

        public BookCover(string id, byte[]? content)
        {
            this.Id = id;
            this.Content = content;
        }
    }
}
