using TextComprehension.Models;
using Action = TextComprehension.Models.Action;

namespace TextComprehension.Test.Helpers;

public static class ModelHelper {
    public static Action GetAction(string action)
    {
        return new Action(action);
    }

    public static Argument[] GetArguments(params string[] arguments)
    {
        return arguments.Select(x => new Argument(x)).ToArray();
    }

    public static Target[] GetTargets(params string[] targets)
    {
        return targets.Select(x => new Target(x)).ToArray();
    }
}