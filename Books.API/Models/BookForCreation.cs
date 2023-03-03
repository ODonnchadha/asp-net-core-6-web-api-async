namespace Books.API.Models
{
    public class BookForCreation
    {
        public Guid AuthorId { get; set; }
        public string Title {  get; set; }
        public string? Description { get; set; }

        public BookForCreation(Guid id, string title, string? description)
        {
            this.AuthorId = id;
            this.Title = title;
            this.Description = description;
        }
    }
}
