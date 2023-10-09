using AutoMapper;
using WebApi.DbOperations;



namespace WebApi.BookOperations.UpdateBook;

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
        //book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId; // if there is already data on book,update
        //book.Title = Model.Title != default ? Model.Title : book.Title;

        _dbContext.SaveChanges();
    }
}


//Model daha anlamlıdır, çünkü sadece belli propertyleri update etmek isteriz.

public class UpdateBookModel
{
    public string Title { get; set; }
    public int GenreId { get; set; }

}
