namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail;

using AutoMapper;
using System.Linq;
using WebApi.DbOperations;

public class GetGenreDetailQuery
{
    public readonly BookStoreDbContext context;
    public readonly IMapper mapper;
    public int genreId { get; set; }

    public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
    {

        this.context = context;
        this.mapper = mapper;

    }

    public GenreDetailViewModel Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.IsActive && x.Id == genreId);

        if (genre is null) { 
            throw new InvalidOperationException("Genre not found!");
           }
        
        return mapper.Map<GenreDetailViewModel>(genre);
        
    }
}

public class GenreDetailViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}