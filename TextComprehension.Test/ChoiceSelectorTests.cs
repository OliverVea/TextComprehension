using TextComprehension.Interfaces;
using TextComprehension.Logic;
using TextComprehension.Models;

namespace TextComprehension.Test;

public class ChoiceSelectorTests
{
    private readonly IChoiceSelector _choiceSelector;

    public ChoiceSelectorTests()
    {
        _choiceSelector = new ChoiceSelector();
    }
    
    [Test]
    public void CanInitialize()
    {
        Assert.That(_choiceSelector, Is.Not.Null);
    }

    [TestCase("look around", true)]
    [TestCase("watch surroundings", true)]
    [TestCase("observe", true)]
    [TestCase("walk", false)]
    [TestCase("look north", false)]
    public void GetChoices_GlobalCommand_ReturnsCorrectGlobalCommand(string command, bool matches)
    {
        // Arrange
        var option = new Option
        {
            Actions = new[] { "look around", "watch surroundings", "observe" }
        };
        
        var context = new ChoiceContext
        {
            GlobalActions = new []{ option }
        };
        
        // Act
        var result = _choiceSelector.GetChoices(command, context);

        // Assert
        Assert.That(result, Is.Not.Null);
        
        var containsOption = result.Choices.Select(x => x.Option).Contains(option);
        Assert.That(containsOption, Is.EqualTo(matches));
    }

    [TestCase("look", 2, true)]
    [TestCase("walk", 1, true)]
    [TestCase("drink", 0, false)]
    public void GetChoices_MultipleGlobalCommands_CorrectCommandIsReturned(string command, int count, bool matches)
    {
        // Arrange
        var optionWalk = new Option { Actions = new[] { "walk" } };
        var optionLook = new Option { Actions = new[] { "look" } };
        
        var context = new ChoiceContext
        {
            GlobalActions = new []{ optionWalk, optionLook, optionLook }
        };
        
        // Act
        var result = _choiceSelector.GetChoices(command, context);

        // Assert
        Assert.That(result, Is.Not.Null);
        
        Assert.That(result.Choices.Count(), Is.EqualTo(count));

        var isInResultSet = result.Choices.Any(x => x.Option.Actions.Contains(command));
        Assert.That(isInResultSet, Is.EqualTo(matches));
    }
}