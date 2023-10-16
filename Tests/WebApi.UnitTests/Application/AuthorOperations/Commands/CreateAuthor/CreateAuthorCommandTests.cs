using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;
public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public CreateAuthorCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenAlreadyExistingAuthorisGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var author = new Author()
        {
            Name = "Test_WhenAlreadyExistingAuthorisGiven",
            Surname = "_InvalidOperationException_ShouldReturn",
            Bio = "Test When Already Existing Author is Given",
            BirthDate = DateTime.Now.Date.AddYears(-100)
        };
        context.Authors.Add(author);
        context.SaveChanges();


        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        command.Model = new CreateAuthorModel()
        {
            Name = author.Name
        };

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author already exists!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Author_ShouldBeCreated()
    {

        //arrange
        CreateAuthorCommand command = new CreateAuthorCommand(context, mapper);
        CreateAuthorModel model = new CreateAuthorModel()
        {
            Name = "Test_WhenAlreadyExistingAuthorisGiven",
            Surname = "_InvalidOperationException_ShouldReturn",
            Bio = "Test When Already Existing Author is Given",
            BirthDate = DateTime.Now.Date.AddYears(-100)
        };

        command.Model = model;

        //act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        //assert
        var author = context.Authors.SingleOrDefault(a => a.Name == model.Name);
        author.Should().NotBeNull();
        author.Name.Should().Be(model.Name);
        author.Surname.Should().Be(model.Surname);
        author.Bio.Should().Be(model.Bio);
        author.BirthDate.Should().Be(model.BirthDate);

    }
}
