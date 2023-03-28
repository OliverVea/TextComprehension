using NUnit.Framework;
using TextComprehension.Models;

namespace TextComprehension.Test.Helpers
{
    internal static class AssertionHelper {
        public static bool AssertEquals(Option expected, Option actual) {
            Assert.That(actual.Action, Is.EqualTo(expected.Action));
            Assert.That(actual.Arguments, Is.EquivalentTo(expected.Arguments));
            Assert.That(actual.CanHaveTarget, Is.EqualTo(expected.CanHaveTarget));
            return true;
        }

        public static bool AssertEquals(Target expected, Target actual) {
            Assert.That(actual.Value, Is.EqualTo(expected.Value));
            return true;
        }
    }
}