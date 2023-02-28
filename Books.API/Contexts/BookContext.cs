using Books.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Books.API.Contexts
{
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; } = null!;
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Author>().HasData(
                new(Guid.Parse("34379433-6c09-4538-bf0d-8cbace8c8019"), "Flann", "O'Brien"),
                new(Guid.Parse("8104c5bf-befa-480a-97b6-c5d9311b34f8"), "William", "Faulkner"),
                new(Guid.Parse("99d115c8-06be-49ef-ac56-6c324c72a707"), "James", "Joyce"),
                new(Guid.Parse("a67882b2-21c0-48a7-89a8-b835ffe27a08"), "G.K.", "Chesterton"));

            builder.Entity<Book>().HasData(
                new(Guid.Parse("e9767333-49be-45b6-8518-a6d072920794"),
                    Guid.Parse("34379433-6c09-4538-bf0d-8cbace8c8019"), "At Swim-Two-Birds", "At Swim-Two-Birds presents itself as a first-person story by an unnamed Irish student of literature. The student believes that 'one beginning and one ending for a book was a thing I did not agree with', and he accordingly sets three apparently quite separate stories in motion."),
                new(Guid.Parse("dfd86c74-f89f-4530-8d0f-a2fd2da7e181"),
                    Guid.Parse("34379433-6c09-4538-bf0d-8cbace8c8019"), "The Third Policeman", "The Third Policeman is set in rural Ireland and is narrated by a dedicated amateur scholar of de Selby, a scientist and philosopher."),
                new(Guid.Parse("c233e878-e224-46d9-b17b-34f36f736f59"),
                    Guid.Parse("8104c5bf-befa-480a-97b6-c5d9311b34f8"), "Absalom, Absalom!", "Absalom, Absalom! details the rise and fall of Thomas Sutpen, a white man born into poverty in western Virginia who moves to Mississippi with the dual aims of gaining wealth and becoming a powerful family patriarch."),
                new(Guid.Parse("8dd56361-4243-4db9-b728-f1e1421704a3"),
                    Guid.Parse("99d115c8-06be-49ef-ac56-6c324c72a707"), "A Portrait Of The Artist As A Young Man", "A Portrait of the Artist as a Young Man is the first novel of Irish writer James Joyce. A Künstlerroman written in a modernist style, it traces the religious and intellectual awakening of young Stephen Dedalus, Joyce's fictional alter ego, whose surname alludes to Daedalus, Greek mythology's consummate craftsman."),
                new(Guid.Parse("4d3d9fcf-ad9f-4c36-b8c4-7353b4cad664"),
                    Guid.Parse("a67882b2-21c0-48a7-89a8-b835ffe27a08"), "The Napoleon Of Notting Hill", "The dreary succession of randomly selected Kings of England is broken up when Auberon Quin, who cares for nothing but a good joke, is chosen. To amuse himself, he institutes elaborate costumes for the provosts of the districts of London"));

            base.OnModelCreating(builder);
        }
    }
}
