using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WebApi.Entities;
using WebApi.TokenOperations.Models;

namespace WebApi.TokenOperations;
public class TokenHandler
{
    readonly IConfiguration _configuration;

    public TokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    //Create encoded credentials
    public Token CreateAccessToken(User user)
    {
        Token tokenModel = new Token();
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));
        SigningCredentials sCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        tokenModel.Expiration = DateTime.Now.AddMinutes(15);

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: _configuration["Token:Issuer"],
            audience: _configuration["Token:Audience"],
            expires: tokenModel.Expiration,
            notBefore: DateTime.Now,
            signingCredentials: sCredentials
            );

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        tokenModel.AccessToken= handler.WriteToken(securityToken ); // Create token
        tokenModel.RefreshToken = CreateRefreshToken();

        return tokenModel;
    }

    public string CreateRefreshToken()
    {
        return Guid.NewGuid().ToString();   
    }
}

