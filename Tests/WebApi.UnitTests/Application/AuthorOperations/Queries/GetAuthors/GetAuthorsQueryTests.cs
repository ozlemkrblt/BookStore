using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.AuthorOperations.Queries.GetAuthors;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.AuthorOperations.Queries.GetAuthors;
public class GetAuthorsQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetAuthorsQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenQueryisHandled_Books_ShouldBeRetrieved()
    {
        // Arrange
        GetAuthorsQuery query = new GetAuthorsQuery(context, mapper);
        var orderedAuthorList = context.Authors.OrderBy(x => x.Id).ToList<Author>();

        //Act
        List<AuthorsViewModel> result = FluentActions.Invoking(() => query.Handle()).Invoke();

        // Assert
        result.Should().NotBeNull();
        for (int i = 0; i < result.Count; i++)
        {

            result[i].FullName.Should().Be(orderedAuthorList[i].Name + " " + orderedAuthorList[i].Surname);

        }
    }

}
