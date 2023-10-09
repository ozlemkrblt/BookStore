using AutoMapper;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;

namespace WebApi.Common;
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        CreateMap<CreateBookModel, Book>();

        CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy")));

        CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
        .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyy")));

        CreateMap<UpdateBookModel, Book>();
    }


}
