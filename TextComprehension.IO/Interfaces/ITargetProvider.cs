using System.Collections.Generic;
using TextComprehension.IO.Models;

namespace TextComprehension.IO.Interfaces
{
    public interface ITargetProvider
    {
        IEnumerable<Target> GetTargets();
    }
}