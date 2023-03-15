using TextComprehension.Models;

namespace TextComprehension.Interfaces;

public interface IProvidedChoiceSelector
{
    ChoiceResult GetChoices(string command);
}