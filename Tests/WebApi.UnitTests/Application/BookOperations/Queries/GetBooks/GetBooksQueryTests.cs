using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBooks;
public class GetBooksQueryTests : IClassFixture<CommonTestFixture>
{
	private readonly BookStoreDbContext context;
	private readonly IMapper mapper;

	public GetBooksQueryTests(CommonTestFixture testFixture)
	{
		context = testFixture.Context;
		mapper = testFixture.Mapper;
	}

	[Fact]
	public void WhenQueryisHandled_Books_ShouldBeRetrieved()
	{
		// Arrange
		GetBooksQuery query = new GetBooksQuery(context, mapper);
        var orderedBookList = context.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList<Book>();
       
        //Act
        List<BooksViewModel> result = FluentActions.Invoking(() => query.Handle()).Invoke();

        // Assert
        result.Should().NotBeNull();
        for (int i=0; i<result.Count; i++)
		{

			result[i].Title.Should().Be(orderedBookList[i].Title);
            result[i].TotalPages.Should().Be(orderedBookList[i].TotalPages);
            result[i].Genre.Should().Be(orderedBookList[i].Genre.Name);
            result[i].PublishDate.Should().Be(orderedBookList[i].PublishDate.ToString("dd/MM/yyyy 00:00:00"));
        }
	}

}
