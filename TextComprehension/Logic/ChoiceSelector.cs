using TextComprehension.Interfaces;
using TextComprehension.Models;

namespace TextComprehension.Logic;

internal sealed class ChoiceSelector : IChoiceSelector
{
    public ChoiceResult GetChoices(string command, ChoiceContext context)
    {
        var matchingOptions = context.GlobalActions.Where(x => MatchesOption(command, x));
        
        return new ChoiceResult
        {
            Choices = matchingOptions.Select(x => new Choice
            {
                Option = x
            }).ToArray()
        };
    }

    private bool MatchesOption(string command, Option option)
    {
        return option.Actions.Contains(command);
    }
}