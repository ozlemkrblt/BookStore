using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;


namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor;

public class CreateAuthorCommand
{

    public CreateAuthorModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;

    public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public void Handle()
    {

        var author = dbContext.Authors.SingleOrDefault(x => x.Name == Model.Name);

        if (author is not null)
            throw new InvalidOperationException("Author already exists!");

        author = mapper.Map<Author>(Model);
        dbContext.Authors.Add(author);
        dbContext.SaveChanges();
    }
}

public class CreateAuthorModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Bio { get; set; }
    public DateTime BirthDate { get; set; }
}


