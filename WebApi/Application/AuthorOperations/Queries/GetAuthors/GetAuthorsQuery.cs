using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors;

public class GetAuthorsQuery
{
    private readonly IBookStoreDbContext _dbContext;
    private readonly IMapper _mapper;
    public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public List<AuthorsViewModel> Handle()
    {
        var authorsList = _dbContext.Authors.OrderBy(x => x.Id).ToList<Author>();
        List<AuthorsViewModel> vm = _mapper.Map<List<AuthorsViewModel>>(authorsList);

        return vm;
    }


}
public class AuthorsViewModel
{
    public String FullName { get; set; }

}
