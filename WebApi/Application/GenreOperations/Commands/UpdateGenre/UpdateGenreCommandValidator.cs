using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre;


public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    { //Kuralýmýzý koþula baðladýk:
        RuleFor(command => command.model.Name).MinimumLength(4).When(x => x.model.Name.Trim() != string.Empty);
    }
}