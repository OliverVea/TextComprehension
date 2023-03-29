using System;
using System.Collections.Generic;

namespace TextComprehension.IO.Models
{
    public class ChoiceContext
    {
        public IEnumerable<Option> Options { get; set; } = Array.Empty<Option>();
        public IEnumerable<Target> Targets { get; set; } = Array.Empty<Target>();
    }
}