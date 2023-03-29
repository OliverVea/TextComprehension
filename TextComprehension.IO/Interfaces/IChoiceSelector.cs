using TextComprehension.IO.Models;

namespace TextComprehension.IO.Interfaces
{
    public interface IChoiceSelector
    {
        ChoiceResult GetChoices(string command, ChoiceContext context);
    }
}