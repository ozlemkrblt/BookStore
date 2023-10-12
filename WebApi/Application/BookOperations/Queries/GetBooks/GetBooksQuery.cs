using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.BookOperations.Queries.GetBooks;

public class GetBooksQuery
{
	private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }


    public List<BooksViewModel> Handle()
	{
        var orderedBookList = _dbContext.Books.Include(x=> x.Genre).OrderBy(x => x.Id).ToList<Book>(); //LinQ
		List<BooksViewModel> vm = _mapper.Map < List<BooksViewModel>>(orderedBookList);//new List<BooksViewModel>();

		return vm;
    }


}

public class BooksViewModel
{

    public String Title { get; set; }

    public int TotalPages { get; set; }

    public String PublishDate { get; set; }

	//GenreId ve Id dönmeyeceğiz, UI'ya döneceğimiz verileri yazdık. 

	public string Genre { get; set; }

}
