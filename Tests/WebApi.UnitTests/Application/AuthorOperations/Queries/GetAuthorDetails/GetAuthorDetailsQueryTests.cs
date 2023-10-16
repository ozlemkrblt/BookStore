using AutoMapper;
using FluentAssertions;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetails;
using WebApi.DbOperations;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetails;
public class GetAuthorDetailsQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorDetailsQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNonExistingAuthorIdisGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var nonExistingAuthorId = int.MaxValue;

        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(context, mapper);
        query.AuthorId = nonExistingAuthorId;

        //act & assert 
        FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author not found!");
    }

    [Fact]
    public void WhenValidIdisGiven_AuthorDetails_ShouldBeRetrieved()
    {
        // Arrange
        GetAuthorDetailsQuery query = new GetAuthorDetailsQuery(context, mapper);
        int authorId = 2;
        query.AuthorId = authorId;
        var author = context.Authors.Where(a => a.Id == authorId).SingleOrDefault();

        //Act
        AuthorDetailsViewModel result = FluentActions.Invoking(() => query.Handle()).Invoke();

        // Assert
        result.Should().NotBeNull();
        result.FullName.Should().Be(author.Name + " " + author.Surname);
        result.Bio.Should().Be(author.Bio);
        result.BirthDate.Should().Be(author.BirthDate.Date.ToString("dd/MM/yyy"));
    }

}
