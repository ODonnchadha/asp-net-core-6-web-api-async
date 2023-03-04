using AutoMapper;

namespace Books.API.Profiles
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<Entities.Book, Models.Book>().ForMember
                (b => b.AuthorName,
                options => options.MapFrom(
                    source => $"{source.Author.LastName}, {source.Author.FirstName}"))
                .ConstructUsing(source => 
                new Models.Book(
                    source.Id, 
                    string.Empty, 
                    source.Title, 
                    source.Description));

            // We can solve by ConstructUsing() and generating the GUID.
            CreateMap<Models.BookForCreation, Entities.Book>()
                .ConstructUsing(source =>
                new Entities.Book(
                    Guid.NewGuid(),
                    source.AuthorId,
                    source.Title,
                    source.Description));

            CreateMap<Entities.Book, Models.BookWithCovers>()
                .ForMember(destination => destination.AuthorName,
                options => options.MapFrom(
                    source => $"{source.Author.LastName}, {source.Author.FirstName}"))
                .ConstructUsing(source => new Models.BookWithCovers(
                    source.Id, string.Empty, source.Title, source.Description));

            CreateMap<Models.External.BookCover, Models.BookCover>();

            CreateMap<IEnumerable<Models.External.BookCover>, Models.BookWithCovers>()
                .ForMember(destimation => destimation.BookCovers,
                options => options.MapFrom(source => source));
        }
    }
}
