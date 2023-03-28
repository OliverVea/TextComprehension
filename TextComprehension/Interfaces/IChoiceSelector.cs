using TextComprehension.Models;

namespace TextComprehension.Interfaces
{
    public interface IChoiceSelector
    {
        ChoiceResult GetChoices(string command, ChoiceContext context);
    }
}