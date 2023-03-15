using TextComprehension.Interfaces;
using TextComprehension.Models;

namespace TextComprehension.Logic;

internal sealed class ChoiceSelector : IChoiceSelector
{
    public ChoiceResult GetChoices(string command, ChoiceContext context)
    {
        var choices = new List<Choice>();

        choices.AddRange(FromActions(command, context));
        choices.AddRange(FromActionsAndArguments(command, context));
        choices.AddRange(FromTargetableActions(command, context));

        return new ChoiceResult
        {
            Choices = choices
        };
    }

    private static IEnumerable<Choice> FromActions(string command, ChoiceContext context)
    {
        return context.Options
            .Where(option => command == option.Action.Value)
            .Select(option => new Choice
        {
            Option = option
        });
    }

    private static IEnumerable<Choice> FromActionsAndArguments(string command, ChoiceContext context)
    {
        return context.Options.SelectMany(option => FromActionAndArguments(command, option));
    }

    private static IEnumerable<Choice> FromActionAndArguments(string command, Option option)
    {
        return option.Arguments
            .Where(argument => $"{option.Action.Value} {argument.Value}" == command)
            .Select(argument => new Choice { Option = option, Argument = argument });
    }

    private static IEnumerable<Choice> FromTargetableActions(string command, ChoiceContext context)
    {
        var choices = new List<Choice>();

        foreach (var option in context.Options)
        {
            if (!option.CanHaveTarget) continue;
            
            foreach (var target in context.Targets)
            {
                if (command == $"{option.Action.Value} {target.Value}")
                    choices.Add(new Choice
                    {
                        Option = option,
                        Target = target
                    });
            }
        }

        return choices;
    }
}