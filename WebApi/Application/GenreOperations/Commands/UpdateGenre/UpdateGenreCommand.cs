using WebApi.DbOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;


public class UpdateGenreCommand
{

    public int GenreId { get; set; }

    public UpdateGenreModel model { get; set; }

    private readonly BookStoreDbContext context;

    public UpdateGenreCommand(BookStoreDbContext context)
    {
        this.context = context;
    }

    public void Handle()
    {
        var genre = context.Genres.FirstOrDefault(x => x.Id == GenreId);
        if (genre is null)
        {
            throw new InvalidOperationException("Genre not found!");
        }
        if (context.Genres.Any(x => x.Name.ToLower() == model.Name.ToLower() && x.Id != GenreId))
        { 
            throw new InvalidOperationException("Genre already exists with a different id.");
        }

        genre.Name = string.IsNullOrEmpty(model.Name.Trim()) ? genre.Name : model.Name;
        genre.IsActive = model.IsActive;
        context.SaveChanges();

    }

}

public class UpdateGenreModel
{
    public String Name { get; set; }
    public bool IsActive { get; set; }
}