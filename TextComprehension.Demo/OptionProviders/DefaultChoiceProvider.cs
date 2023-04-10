using TextComprehension.Demo.Options;
using TextComprehension.IO.Interfaces;
using TextComprehension.IO.Models;

namespace TextComprehension.Demo.ChoiceProviders;

public class DefaultChoiceProvider : IOptionProvider
{
    public IEnumerable<Option> GetOptions()
    {
        return new Option[]
        {
            new HelpOption(),
            new ForwardOption(),
            new LeftOption(),
            new RightOption(),
            new TakePictureOption()
        };
    }
}