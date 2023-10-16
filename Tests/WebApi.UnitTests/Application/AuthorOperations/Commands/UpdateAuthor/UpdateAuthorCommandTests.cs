using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DbOperations;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;
public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public UpdateAuthorCommandTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNonExistingAuthorisGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var nonExistingAuthorId = int.MaxValue;

        UpdateAuthorCommand command = new UpdateAuthorCommand(context, mapper);
        command.authorId = nonExistingAuthorId;

        //act & assert 
        FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author to update is not found!");
    }

    [Fact]
    public void WhenValidInputAreGiven_Authýr_ShouldBeUpdated()
    {
        // Arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(context, mapper);

        command.authorId = 2;
        UpdateAuthorModel model = new UpdateAuthorModel()
        {
            Name = "Jane",
            Surname = "Austen",
            BirthDate = new DateTime(1775, 12, 16),
            Bio = "Jane Austen, English writer who first gave the novel its distinctly modern character through her treatment of ordinary people in everyday life. "

        };
        command.Model = model;


        //Act
        FluentActions.Invoking(() => command.Handle()).Invoke();

        // Assert
        var updatedAuthor = context.Authors.SingleOrDefault(auth => auth.Name == model.Name);
        updatedAuthor.Should().NotBeNull();
        updatedAuthor.Name.Should().Be(model.Name);
        updatedAuthor.Surname.Should().Be(model.Surname);
        updatedAuthor.Bio.Should().Be(model.Bio);
        updatedAuthor.BirthDate.Should().Be(model.BirthDate);


    }




}
