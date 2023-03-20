using TextComprehension.Interfaces;
using TextComprehension.Models;

namespace TextComprehension.Logic;

public class ProvidedChoiceSelector : IProvidedChoiceSelector
{
    private readonly IChoiceSelector _choiceSelector;

    private readonly HashSet<IOptionProvider> _optionProviders = new HashSet<IOptionProvider>();
    private readonly HashSet<ITargetProvider> _targetProviders = new HashSet<ITargetProvider>();

    public ProvidedChoiceSelector(IChoiceSelector choiceSelector)
    {
        _choiceSelector = choiceSelector;
    }

    public void AddOptionProvider(IOptionProvider choiceProvider)
    {
        _optionProviders.Add(choiceProvider);
    }

    public void AddTargetProvider(ITargetProvider targetProvider)
    {
        _targetProviders.Add(targetProvider);
    }

    public ChoiceResult GetChoices(string command)
    {
        if (!_optionProviders.Any()) return new ChoiceResult();

        var options = _optionProviders.SelectMany(x => x.GetOptions());
        var targets = _targetProviders.SelectMany(x => x.GetTargets());

        var context = new ChoiceContext
        {
            Options = options,
            Targets = targets
        };

        return _choiceSelector.GetChoices(command, context);
    }
}