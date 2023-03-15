namespace TextComprehension.Models;

public class ChoiceResult
{
    public IEnumerable<Choice> Choices { get; init; } = Array.Empty<Choice>();
}