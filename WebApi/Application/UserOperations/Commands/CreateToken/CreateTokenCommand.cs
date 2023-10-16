using AutoMapper;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.CreateToken;
public class CreateTokenCommand
{

    public CreateTokenModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;
    readonly IConfiguration configuration;
    public CreateTokenCommand(IBookStoreDbContext dbContext, IMapper mapper, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
        this.configuration = configuration;
    }
    public Token  Handle()
    {

        var user = dbContext.Users.FirstOrDefault(x => x.Email == Model.Email && x.Password == Model.Password);

        if (user is not null)
        {
            TokenHandler handler = new TokenHandler(configuration);
            Token token = handler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
            dbContext.SaveChanges();

            return token;
        }
        else
        {
            throw new InvalidOperationException("Wrong credentials!");
        }
    }
}

public class CreateTokenModel
{
    public string Email { get; set; }
    public string Password { get; set; }
}

