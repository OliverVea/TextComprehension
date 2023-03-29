using TextComprehension.IO.Models;

namespace TextComprehension.IO.Interfaces
{
    public interface IProvidedChoiceSelector
    {
        void AddOptionProvider(IOptionProvider choiceProvider);
        void AddTargetProvider(ITargetProvider targetProvider);
        ChoiceResult GetChoices(string command);
    }
}