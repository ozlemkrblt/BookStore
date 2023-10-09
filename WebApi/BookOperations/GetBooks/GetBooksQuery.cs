using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBooks;

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
        var orderedBookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>(); //LinQ
		List<BooksViewModel> vm = _mapper.Map < List<BooksViewModel>>(orderedBookList);//new List<BooksViewModel>();
		//foreach (var book in orderedBookList)
		//{
		//	vm.Add(new BooksViewModel()
		//	{
		//		Title = book.Title,
		//		Genre = ((GenreEnum)book.GenreId).ToString(),
		//		PublishDate = book.PublishDate.Date.ToString("dd/MM/yyy"), //sadece tarih kısmını alacağız.
		//		TotalPages = book.TotalPages,
		//	});
		//}
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
