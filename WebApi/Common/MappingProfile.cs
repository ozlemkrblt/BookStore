using AutoMapper;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;

using WebApi.Entities;

using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.Common;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateBookModel, Book>();

        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
             .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy")));
        

        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy")));

        CreateMap<UpdateBookModel, Book>();

        CreateMap<Genre, GenresViewModel>();
        CreateMap<Genre, GenreDetailViewModel>();

        CreateMap<CreateAuthorModel, Author>();
        CreateMap<Author, AuthorsViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname));
        CreateMap<Author, AuthorDetailsViewModel>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Name + " " + src.Surname))
        .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => src.BirthDate.Date.ToString("dd/MM/yyy")));

        CreateMap<UpdateAuthorModel, Author>();
    }


}
