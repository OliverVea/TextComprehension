namespace TextComprehension.Models;

public class Choice
{
    public Option Option { get; init; } = new();
    public Argument Argument { get; init; } = new(string.Empty);
    public Target Target { get; init; } = new(string.Empty);
}