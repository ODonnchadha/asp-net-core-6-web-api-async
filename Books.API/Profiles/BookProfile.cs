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
                .ConstructUsing(source => new Models.Book(
                    source.Id, string.Empty, source.Title, source.Description));
        }
    }
}
