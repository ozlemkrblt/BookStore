using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre;

public class CreateGenreCommand
{
    public CreateGenreModel model { get; set; }

    private readonly IBookStoreDbContext context;

    public CreateGenreCommand(IBookStoreDbContext bookStoreDbContext)
    {
        this.context = bookStoreDbContext;

    }


    public void Handle()
    {
        var genre = context.Genres.SingleOrDefault(x => x.Name == model.Name);
        if (genre is not null)
            throw new InvalidOperationException("Genre already exists!");


        genre = new Genre();
        genre.Name = model.Name;
        context.Genres.Add(genre);
        context.SaveChanges();
    }
}


public class CreateGenreModel {

    public string Name { get; set; }
} 