using System;
using System.Collections.Generic;

namespace TextComprehension.Models
{
    public class ChoiceResult
    {
        public IEnumerable<Choice> Choices { get; set; } = Array.Empty<Choice>();
    }
}