using AutoMapper;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
public class GetAuthorDetailsQuery
{
    private readonly BookStoreDbContext context;
    public int AuthorId { get; set; }
    private readonly IMapper mapper;
    public GetAuthorDetailsQuery(BookStoreDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public AuthorDetailsViewModel Handle()
    {
        var author = context.Authors.Where(author => author.Id == AuthorId).SingleOrDefault();

        if (author == null)
        {
            throw new InvalidOperationException("Author not found!");
        }

        AuthorDetailsViewModel viewModel = mapper.Map<AuthorDetailsViewModel>(author);
        return viewModel;
    }
}

public class AuthorDetailsViewModel
{
    public String FullName { get; set; }
    public String Bio { get; set; }
    public String BirthDate { get; set; }
}


