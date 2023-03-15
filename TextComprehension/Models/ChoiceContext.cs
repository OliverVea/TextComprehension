namespace TextComprehension.Models;

public class ChoiceContext
{
    public IEnumerable<Option> Options { get; init; } = Array.Empty<Option>();
    public IEnumerable<Target> Targets { get; init; } = Array.Empty<Target>();
}