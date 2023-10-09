using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; //sadece constructor içinde set edilebilirler.

        public BookController(BookStoreDbContext context)
        {
            _context = context; 
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result= query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            var book = _context.Books.Where(book=> book.Id== id).SingleOrDefault(); //LinQ
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

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.Model = newBook;
                command.Handle();
                
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok(); 

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == updatedBook.Id);
            if (book is null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId ; // if there is already data on book,update
            book.TotalPages = updatedBook.TotalPages != default ? updatedBook.TotalPages : book.TotalPages;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;

            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book is null)
                return BadRequest();


            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }
    }



}