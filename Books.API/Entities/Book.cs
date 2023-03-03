using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.API.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key()]
        public Guid Id { get; set; }

        [Required(), MaxLength(160)]
        public string Title { get; set; }

        [MaxLength(2500)]
        public string? Description { get; set; }

        public Guid AuthorId { get; set; }

        public Author Author { get; set; } = null!;

        public Book(Guid id, Guid authorId, string title, string? description)
        {
            Id = id;
            AuthorId = authorId;
            Title = title;
            Description = description;
        }
        // We could perhaps allow other pieces of code to construct this object in this manner.
        // And all for AutoMapper();
        //public Book(Guid authorId, string title, string? description)
        //{
        //    AuthorId = authorId;
        //    Title = title;
        //    Description = description;
        //}
    }
}
