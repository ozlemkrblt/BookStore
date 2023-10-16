﻿using AutoMapper;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.UserOperations.Commands.CreateUser;
public class CreateUserCommand
{

    public CreateUserModel Model { get; set; }
    private readonly IBookStoreDbContext dbContext;
    private readonly IMapper mapper;
    public CreateUserCommand(IBookStoreDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public void Handle()
    {

        var user = dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);

        if (user is not null)
            throw new InvalidOperationException("User already exists!");

        user = mapper.Map<User>(Model);


        dbContext.SaveChanges();
    }
}

public class CreateUserModel
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

