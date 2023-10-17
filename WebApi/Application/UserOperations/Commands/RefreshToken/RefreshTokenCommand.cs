using AutoMapper;
using WebApi.DbOperations;
using WebApi.TokenOperations;
using WebApi.TokenOperations.Models;

namespace WebApi.Application.UserOperations.Commands.RefreshToken;
public class RefreshTokenCommand
{

    public string RefreshToken { get; set; }
    private readonly IBookStoreDbContext dbContext;
    readonly IConfiguration configuration;
    public RefreshTokenCommand(IBookStoreDbContext dbContext, IConfiguration configuration)
    {
        this.dbContext = dbContext;
        this.configuration = configuration;
    }
    public Token Handle()
    {

        var user = dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);

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
            throw new InvalidOperationException("Valid refresh token cannot found!");
        }
    }
}
