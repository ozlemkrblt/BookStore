using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;


namespace WebApi.Application.BookOperations.Commands.UpdateBook;

    public class UpdateBookCommand
    {
   
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int bookId { get; set; }

    public UpdateBookModel Model { get; set; }
    public UpdateBookCommand(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var book = _dbContext.Books.SingleOrDefault(x => x.Id == bookId);
        if (book is null)
            throw new InvalidOperationException("Book to update is not found!");
        book = _mapper.Map<Book>(Model);

        
        if (Model.AuthorIds != null && Model.AuthorIds.Any())
        {
            // If there are new authors to be updated, clear the Authors list in book, and update it 
            book.Authors.Clear();
            var authors = _dbContext.Authors.Where(a => Model.AuthorIds.Contains(a.Id)).ToList();

            foreach (var author in authors)
            {
                book.Authors.Add(author);
            }
        }

        _dbContext.SaveChanges();
    }
}


//model daha anlamlıdır, çünkü sadece belli propertyleri update etmek isteriz.

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }
    public List<int> AuthorIds { get; set; }    = new List<int>();

}
