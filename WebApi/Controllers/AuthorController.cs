using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using AutoMapper;
using FluentValidation;

namespace WebApi.AddControllers;

[ApiController]
[Route("[controller]s")]
public class AuthorController : ControllerBase
{
    private readonly BookStoreDbContext _context;

    private readonly IMapper _mapper;
    public AuthorController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        AuthorDetailsViewModel result;

        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(_context, _mapper);
        query.AuthorId = id;
        GetAuthorDetailsQueryValidator validator = new GetAuthorDetailsQueryValidator();
        validator.ValidateAndThrow(query);
        result = query.Handle();

        return Ok(result);
    }



    [HttpPost]
    public IActionResult AddAuthor([FromBody] CreateAuthorModel newAuthor)
    {
        CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);

        command.Model = newAuthor;
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();

    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updatedAuthor)
    {

        UpdateAuthorCommand command = new UpdateAuthorCommand(_context, _mapper);
        command.authorId = id;
        command.Model = updatedAuthor;

        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();

    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {

        DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
        command.authorId = id;

        DeleteAuthorCommandValidator validator = new DeleteAuthorCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();

        return Ok();

    }
}



