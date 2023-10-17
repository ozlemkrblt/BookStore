using Microsoft.AspNetCore.Mvc;
using WebApi.DbOperations;
using AutoMapper;
using FluentValidation;
using WebApi.Application.UserOperations.Commands.CreateUser;
using Microsoft.Extensions.Configuration;
using WebApi.Application.UserOperations.Commands.CreateToken;
using WebApi.Application.UserOperations.Commands.RefreshToken;
using WebApi.TokenOperations.Models;
using Microsoft.AspNetCore.Authorization;
//using WebApi.Application.UserOperations.Commands.DeleteUser;
//using WebApi.Application.UserOperations.Commands.UpdateUser;

namespace WebApi.AddControllers;
[Authorize]
[ApiController]
[Route("[controller]s")]
public class UserController : Controller
{
    private readonly IBookStoreDbContext _context;

    private readonly IMapper _mapper;

    readonly IConfiguration _configuration;
    public UserController(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }


    [HttpPost]
    public IActionResult AddUser([FromBody] CreateUserModel newUser)
    {
        CreateUserCommand command = new CreateUserCommand(_context, _mapper);

        command.Model = newUser;
        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }


    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
    {
        CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);

        command.Model = login;

        var token = command.Handle();
        return token;


    }


    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] String token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);

        command.RefreshToken = token;

        var resultToken = command.Handle();
        return resultToken;


    }
}
