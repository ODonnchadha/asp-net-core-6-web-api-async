﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Books.API.Entities
{
    [Table("Authors")]
    public class Author
    {
        [Key()]
        public Guid Id { get; set; }

        [Required(), MaxLength(160)]
        public string FirstName { get; set; }

        [Required(), MaxLength(160)]
        public string LastName { get; set; }

        public Author(Guid id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
