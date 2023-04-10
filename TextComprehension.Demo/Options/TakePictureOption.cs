using TextComprehension.IO.Models;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.Demo.Options;

public class TakePictureOption : Option
{
    public TakePictureOption()
    {
        Action = new Action("take picture");
    }
}