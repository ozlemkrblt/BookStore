﻿namespace WebApi.Application.BookOperations.Commands.CreateBook;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApi.Common;
using WebApi.DbOperations;
using WebApi.Entities;

//Burada ViewModel kullanmayacağız, çünkü sadece kullanıcıya dönmüyoruz.
public class CreateBookCommand
{

    public CreateBookModel Model { get; set; }
    private readonly BookStoreDbContext dbContext;
    private readonly IMapper mapper;
    public CreateBookCommand(BookStoreDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public void Handle()
    {

        var book = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
        
        if (book is not null)
            throw new InvalidOperationException("Book already exists!");

        book = mapper.Map<Book>(Model); //new Book();
        //book.Title = model.Title;
        //book.PublishDate = model.PublishDate;
        //book.GenreId=model.GenreId;
        //book.TotalPages=model.TotalPages;

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

