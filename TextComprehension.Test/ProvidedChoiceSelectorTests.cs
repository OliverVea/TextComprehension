using TextComprehension.Interfaces;
using TextComprehension.Logic;

namespace TextComprehension.Test;

public class ProvidedChoiceSelectorTests
{
    private readonly IProvidedChoiceSelector _choiceSelector;

    public ProvidedChoiceSelectorTests()
    {
        _choiceSelector = new ProvidedChoiceSelector();
    }

    [Test]
    public void GetChoices_NoProviders_EmptyResult()
    {
        // Arrange
        
        // Act
        var result = _choiceSelector.GetChoices("");
        
        // Assert
        Assert.IsNotNull(result);
        Assert.IsEmpty(result.Choices);
    }
}