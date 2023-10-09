namespace WebApi.BookOperations.CreateBook;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;


//Burada ViewModel kullanmayacağız, çünkü sadece kullanıcıya dönmüyoruz.
public class CreateBookCommand
{

    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext dbContext; 
    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void Handle()
    {

        var book = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        if (book is not null)
            throw new InvalidOperationException("Book already exists!");
        book = new Book();
        book.Title = Model.Title;
        book.PublishDate = Model.PublishDate;
        book.GenreId=Model.GenreId;
        book.TotalPages=Model.TotalPages;

        dbContext.Books.Add(book);
        dbContext.SaveChanges();
    }
}

public class CreateBookModel
{
    public string Title{ get; set; }
    public int GenreId { get; set; }

    public int TotalPages {  get; set; }   
    public DateTime PublishDate { get; set; }
}


