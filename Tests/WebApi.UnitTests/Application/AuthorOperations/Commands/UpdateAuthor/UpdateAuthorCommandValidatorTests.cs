using FluentAssertions;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;

namespace WebApi.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor;
public class UpdateBookCommandValidatorTests
{

    [Theory]
    [InlineData("Jane", 0, "")]
    [InlineData("", 1, "One morning, when Gregor Samsa woke from troubled dreams, he found himself transformed  in his bed into a horrible vermin. He lay on his armour-like back, and if he lifted his head a little he could see.")]
    [InlineData(" ", 1, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qua.")]
    [InlineData(" ", -1, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qua.")]
    [InlineData("", -1, "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec qua.")]
    [InlineData("Jane", 2, "Far far away, behind the word mountains, far from the countries Vokalia and Consonantia, there l")] //right data

    public void WhenInvalidInputAreGiven_Validator_ShouldReturnErrors(string name, int id, string bio)
    {
        //arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.Model = new UpdateAuthorModel()
        {
            Name = name,
            Surname="Can be empty",
            Bio = bio
        };

        command.authorId = id;

        //act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);


        //assert 
        result.Errors.Count.Should().BeGreaterThan(0);

    }

    [Fact]
    public void WhenDateTimeEqualNowisGiven_Validator_ShouldReturnError()
    {
        //arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.Model = new UpdateAuthorModel()
        {
            Name = "Jane",
            Surname = "Austen",
            BirthDate = DateTime.Now,
            Bio = "Jane Austen, English writer who first gave the novel its distinctly modern character through her treatment of ordinary people in everyday life. "

        };
        command.authorId = 2;
        
        //act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().BeGreaterThan(0);
    }
    [Fact]
    public void WhenValidInputAreGiven_Validator_ShouldNotReturnError()
    {
        //arrange
        UpdateAuthorCommand command = new UpdateAuthorCommand(null, null);
        command.Model = new UpdateAuthorModel()
        {
            Name = "Jane",
            Surname = "Austen",
            BirthDate = new DateTime(1775, 12, 16),
            Bio = "Jane Austen, English writer who first gave the novel its distinctly modern character through her treatment of ordinary people in everyday life. "

        };
        command.authorId = 2;
        //act
        UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
        var result = validator.Validate(command);

        result.Errors.Count.Should().Be(0);
    }
}

