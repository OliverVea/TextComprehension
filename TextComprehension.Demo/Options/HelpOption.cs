using TextComprehension.IO.Models;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.Demo.Options;

public class HelpOption : Option
{
    public HelpOption()
    {
        Action = new Action("help");
    }
}