using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

public class UpdateAuthorCommand
{

    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public int authorId { get; set; }

    public UpdateAuthorModel Model { get; set; }
    public UpdateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public void Handle()
    {
        var author = _dbContext.Authors.SingleOrDefault(x => x.Id == authorId);
        if (author is null)
            throw new InvalidOperationException("Author to update is not found!");

        author = _mapper.Map<Author>(Model);

        _dbContext.SaveChanges();
    }
}

public class UpdateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Bio { get; set; }
    public DateTime BirthDate { get; set; }

}
