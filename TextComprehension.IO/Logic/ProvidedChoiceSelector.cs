using System.Collections.Generic;
using System.Linq;
using TextComprehension.IO.Interfaces;
using TextComprehension.IO.Models;

namespace TextComprehension.IO.Logic
{
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
}