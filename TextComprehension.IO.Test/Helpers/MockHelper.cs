using Moq;
using TextComprehension.IO.Interfaces;
using TextComprehension.IO.Models;

namespace TextComprehension.IO.Test.Helpers
{
    public static class MockHelper
    {
        public static IOptionProvider MockOptionProvider(params Option[] options)
        {
            var mock = new Mock<IOptionProvider>();
            mock.Setup(x => x.GetOptions()).Returns(options);
            return mock.Object;
        }

        internal static ITargetProvider MockTargetProvider(params Target[] targets)
        {
            var mock = new Mock<ITargetProvider>();
            mock.Setup(x => x.GetTargets()).Returns(targets);
            return mock.Object;
        }
    }
}