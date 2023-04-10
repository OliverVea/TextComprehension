using TextComprehension.IO.Models;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.Demo.Options;

public class ForwardOption : Option
{
    public ForwardOption()
    {
        Action = new Action("forward");
    }
}