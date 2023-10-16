using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.DbOperations;

namespace WebApi.UnitTests.Application.BookOperations.Queries.GetBookDetail;
public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
{
	private readonly BookStoreDbContext context;
	private readonly IMapper mapper;

	public GetBookDetailQueryTests(CommonTestFixture testFixture)
	{
		context = testFixture.Context;
		mapper = testFixture.Mapper;
	}

	[Fact]
	public void WhenNotExistingBookIdGiven_InvalidOperationException_ShouldReturn()
	{
		//arrange
		var nonExistingBookId = int.MaxValue;

		GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
		query.BookId = nonExistingBookId;

		//act & assert 
		FluentActions
			.Invoking(() => query.Handle())
			.Should().Throw<InvalidOperationException>().And.Message.Should().Be("Book not found!");
	}

	[Fact]
	public void WhenValidIdisGiven_BookDetails_ShouldBeRetrieved()
	{
		// Arrange
		GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
		int BookId = 2;
        query.BookId = BookId;
		var book = context.Books.Include(x => x.Genre).Include(x => x.Authors).Where(b => b.Id == BookId ).SingleOrDefault();
		
		//Act
		BookDetailViewModel result = FluentActions.Invoking(() => query.Handle()).Invoke();

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(book.Title);
		result.TotalPages.Should().Be(book.TotalPages);
        result.Genre.Should().Be(book.Genre.Name);
		result.Authors.Count.Should().Be(book.Authors.Count);
		result.PublishDate.Should().Be(book.PublishDate.ToString("dd/MM/yyyy 00:00:00"));

	}

}
