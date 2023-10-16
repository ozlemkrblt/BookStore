using AutoMapper;
using FluentAssertions;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.GenreOperations.Queries.GetGenres;
public class GetGenresQueryTests : IClassFixture<CommonTestFixture>
{
    private readonly BookStoreDbContext context;
    private readonly IMapper mapper;

    public GetGenresQueryTests(CommonTestFixture testFixture)
    {
        context = testFixture.Context;
        mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenQueryisHandled_Genres_ShouldBeRetrieved()
    {
        // Arrange
        GetGenresQuery query = new GetGenresQuery(context, mapper);
        var orderedGenreList = context.Genres.OrderBy(x => x.Id).ToList<Genre>();

        //Act
        List<GenresViewModel> result = FluentActions.Invoking(() => query.Handle()).Invoke();

        // Assert
        result.Should().NotBeNull();
        for (int i = 0; i < result.Count; i++)
        {

            result[i].Id.Should().Be(orderedGenreList[i].Id);
            result[i].Name.Should().Be(orderedGenreList[i].Name);
        }
    }

}
