using TextComprehension.IO.Models;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.Demo.Options;

public class LeftOption : Option
{
    public LeftOption()
    {
        Action = new Action("left");
    }
}