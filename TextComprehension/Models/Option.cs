namespace TextComprehension.Models;

public class Option
{
    public Action Action { get; init; } = new(string.Empty);
    public IReadOnlyList<Argument> Arguments { get; init; } = Array.Empty<Argument>();
    public bool CanHaveTarget { get; init; }
}