using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {

        private static List<Book> BookList = new List<Book>()
        {
        new Book{
            Id=1,
            Title="Lean Startup",
            GenreId=1, //Id 1 = Personal growth
            TotalPages=200,
            PublishDate=new DateTime(2001,06,12)
        },
        new Book{
            Id=2,
            Title="Herland",
            GenreId=2, //Id 2=Science Fiction
            TotalPages=250,
            PublishDate=new DateTime(2010,05,23)
        },
        new Book{
            Id=3,
            Title="Dune",
            GenreId=2, //Id 2=Science Fiction
            TotalPages=540,
            PublishDate=new DateTime(1957,07,05)
        }
        };

        [HttpGet]
        public List<Book> GetBooks()
        {
            var orderedBookList = BookList.OrderBy(x => x.Id).ToList<Book>(); //LinQ
            return orderedBookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = BookList.Where(book=> book.Id== id).SingleOrDefault(); //LinQ
            return book;
        }

        /*
        [HttpGet] //sadece 1 tane parametresiz get olmalý!
        public Book Get([FromQuery]string id)
        {
            var book = BookList.Where(book => book.Id == Convert.ToInt32( id)).SingleOrDefault(); //LinQ
            return book;
        }
        */

    }



}