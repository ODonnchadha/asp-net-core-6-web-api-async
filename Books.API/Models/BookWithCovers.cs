﻿namespace Books.API.Models
{
    public class BookWithCovers : Book
    {
        public IEnumerable<BookCover> BookCovers { get; set; } = new List<BookCover>();
        public BookWithCovers(
            Guid id, string authorName, string title, string? description) : 
            base(id, authorName, title, description) { }
    }
}
