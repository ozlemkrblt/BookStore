using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail;

    public class GetBookDetailQuery
    {
    private readonly BookStoreDbContext context;
    public int BookId {  get; set; }
    public GetBookDetailQuery(BookStoreDbContext context)
    {
        this.context = context;
    }

    public BookDetailViewModel Handle()
    {
        var book = context.Books.Where(book => book.Id == BookId).SingleOrDefault(); //LinQ
        if(book == null)
        {
            throw new InvalidOperationException("Book not found!");
        }
        BookDetailViewModel viewModel = new BookDetailViewModel();  
        viewModel.Title = book.Title;
        viewModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        viewModel.TotalPages = book.TotalPages;
        viewModel.Genre= ((GenreEnum)book.GenreId).ToString();
        
        
        
        return viewModel;
    }
    }

public class BookDetailViewModel
{
    public String Title { get; set; }

    public int TotalPages { get; set; }

    public String PublishDate { get; set; }

    //GenreId ve Id dönmeyeceğiz, UI'ya döneceğimiz verileri yazdık. 

    public string Genre { get; set; }
}


