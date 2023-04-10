using TextComprehension.IO.Models;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.Demo.Options;

public class RightOption : Option
{
    public RightOption()
    {
        Action = new Action("right");
    }
}