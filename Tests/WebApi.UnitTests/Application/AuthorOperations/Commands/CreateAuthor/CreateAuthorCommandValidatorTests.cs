using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;


namespace WebApi.UnitTests.Application.BookOperations.Commands.CreateBook;
public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
{

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed  in his bed into a horrible vermin. He lay on his armour-like back, and if he lifted his head a little he could see.")]
    [InlineData(null, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qua.")]
    [InlineData("Jane", "")]
    [InlineData("", "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there l")]
    public void WhenInvalidInputAreGiven_Validator_ShouldReturnErrors(string name, string bio)
    {
        //arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = name,
            Surname = "Can be empty",
            BirthDate = DateTime.Now.Date.AddYears(-100),
            Bio = bio

        };

        //act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);


        //assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenDateTimeEqualNowisGiven_Validator_ShouldReturnError()
    {
        //arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = "Jane",
            Surname = "Austen",
            BirthDate = DateTime.Now,
            Bio = "Jane Austen, English writer who first gave the novel its distinctly modern character through her treatment of ordinary people in everyday life. "

        };

        //act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }

    [Fact]
    public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
    {
        //arrange
        CreateAuthorCommand command = new CreateAuthorCommand(null, null);
        command.Model = new CreateAuthorModel()
        {
            Name = "Jane",
            Surname = "Austen",
            BirthDate = new DateTime(1775, 12, 16),
            Bio = "Jane Austen, English writer who first gave the novel its distinctly modern character through her treatment of ordinary people in everyday life. "

        };

        //act
        CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}

