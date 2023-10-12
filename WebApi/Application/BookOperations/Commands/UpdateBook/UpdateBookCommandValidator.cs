﻿using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
    {

        RuleFor(command => command.bookId).NotEmpty().GreaterThan(0);
        RuleFor(command => command.Model.GenreId).GreaterThan(0);
        RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(3);

    }
}

