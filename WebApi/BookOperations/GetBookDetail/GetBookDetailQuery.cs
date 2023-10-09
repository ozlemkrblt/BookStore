using AutoMapper;
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
    private readonly IMapper mapper;
    public GetBookDetailQuery(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public BookDetailViewModel Handle()
    {
        var book = context.Books.Where(book => book.Id == BookId).SingleOrDefault(); //LinQ
        
        if(book == null)
        {
            throw new InvalidOperationException("Book not found!");
        }

        BookDetailViewModel viewModel = mapper.Map<BookDetailViewModel>(book);  
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


