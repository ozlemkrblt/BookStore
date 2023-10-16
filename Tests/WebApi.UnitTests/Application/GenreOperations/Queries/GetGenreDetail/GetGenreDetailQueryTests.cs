using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DbOperations;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenreDetail;
public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenreDetailQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenNotExistingBookIdGiven_InvalidOperationException_ShouldReturn()
    {
        //arrange
        var nonExistingGenreId = int.MaxValue;

        GetGenreDetailQuery query = new GetGenreDetailQuery(context, mapper);
        query.genreId = nonExistingGenreId;

        //act & assert 
        FluentActions
            .Invoking(() => query.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre not found!");
    }

    [Fact]
    public void WhenValidIdisGiven_GenreDetails_ShouldBeRetrieved()
    {
        // Arrange
        GetGenreDetailQuery query = new GetGenreDetailQuery(context, mapper);
        int genreId = 2;
        query.genreId = genreId;
        var genre = context.Genres.Where(b => b.Id == genreId).SingleOrDefault();

        //Act
        GenreDetailViewModel result = FluentActions.Invoking(() => query.Handle()).Invoke();

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(genre.Id);
        result.Name.Should().Be(genre.Name);

    }

}
