using System.Collections.Generic;
using TextComprehension.Models;

namespace TextComprehension.Interfaces
{
    public interface IOptionProvider
    {
        IEnumerable<Option> GetOptions();
    }
}