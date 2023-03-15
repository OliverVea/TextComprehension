using TextComprehension.Interfaces;
using TextComprehension.Models;

namespace TextComprehension.Logic;

public class ProvidedChoiceSelector : IProvidedChoiceSelector
{
    private readonly IChoiceSelector _choiceSelector;

    public ProvidedChoiceSelector()
    {
        _choiceSelector = new ChoiceSelector();
    }

    public ChoiceResult GetChoices(string command)
    {
        return new ChoiceResult();
    }
}