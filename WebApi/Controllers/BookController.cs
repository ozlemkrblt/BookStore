using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.UpdateBook;
using System.Linq.Expressions;
using WebApi.BookOperations.DeleteBook;
using AutoMapper;
using FluentValidation.Results;
using FluentValidation;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context; //sadece constructor içinde set edilebilirler.

        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
                result = query.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

            return Ok(result);
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
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            try
            {
                command.Model = newBook;
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
                //if (!result.IsValid)
                //{
                //    foreach(var item in result.Errors)
                //    {
                //        Console.WriteLine("Property: " + item.PropertyName + "- Error Message: " + item.ErrorMessage);
                //    }
                //}else 
                //    command.Handle();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {
            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context , _mapper);
                command.bookId = id;
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.bookId = id;
                DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
                validator.ValidateAndThrow(command);
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();

        }
    }



}