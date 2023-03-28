using System;
using System.Collections.Generic;

namespace TextComprehension.Models
{
    public class Option
    {
        public Action Action { get; set; } = Action.Default;
        public IReadOnlyList<Argument> Arguments { get; set; } = Array.Empty<Argument>();
        public bool CanHaveTarget { get; set; }
    }
}