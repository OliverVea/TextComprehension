using System;
using System.Collections.Generic;

namespace TextComprehension.IO.Models
{
    public class ChoiceResult
    {
        public IEnumerable<Choice> Choices { get; set; } = Array.Empty<Choice>();
    }
}