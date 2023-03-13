using TextComprehension.Interfaces;

namespace TextComprehension.Models;

public class ChoiceContext
{
    public IEnumerable<Option> GlobalActions { get; init; } = Array.Empty<Option>();
}