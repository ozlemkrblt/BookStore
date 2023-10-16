using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
public class DeleteAuthorCommand
{

    private readonly IBookStoreDbContext _dbContext;
    public int authorId { get; set; }
    public DeleteAuthorCommand(IBookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == authorId);
        if (author is null)
            throw new InvalidOperationException("Author to delete is not found!");

        if (_dbContext.Books.Any(b => b.Authors.Any(a => a.Id == authorId)))
        {
            throw new InvalidOperationException("This author has a published book. If you want to delete author , please remove book first.");
        }

        _dbContext.Authors.Remove(author);
        _dbContext.SaveChanges();
    }


}