using TextComprehension.Models;

namespace TextComprehension.Interfaces
{
    public interface IProvidedChoiceSelector
    {
        void AddOptionProvider(IOptionProvider choiceProvider);
        void AddTargetProvider(ITargetProvider targetProvider);
        ChoiceResult GetChoices(string command);
    }
}