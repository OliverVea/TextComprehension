using System.Collections.Generic;
using TextComprehension.Models;

namespace TextComprehension.Interfaces
{
    public interface ITargetProvider
    {
        IEnumerable<Target> GetTargets();
    }
}