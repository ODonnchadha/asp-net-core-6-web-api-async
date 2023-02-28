namespace Books.API.Models
{
    public class Book
    {
        public Guid Id { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        public Book(Guid id, string authorName, string title, string? description)
        {
            Id = id;
            AuthorName = authorName;
            Title = title;
            Description = description;
        }
    }
}
