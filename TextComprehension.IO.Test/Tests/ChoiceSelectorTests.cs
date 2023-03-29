using TextComprehension.IO.Interfaces;
using TextComprehension.IO.Logic;
using TextComprehension.IO.Models;
using TextComprehension.IO.Test.Helpers;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.IO.Test.Tests
{
    public class ChoiceSelectorTests
    {
        private IChoiceSelector _choiceSelector = null!;

        [SetUp]
        public void CreateChoiceSelector()
        {
            _choiceSelector = new ChoiceSelector();
        }
        
        [Test]
        public void CanInitialize()
        {
            Assert.That(_choiceSelector, Is.Not.Null);
        }

        [TestCase("look around", true)]
        [TestCase("watch surroundings", true)]
        [TestCase("observe", true)]
        [TestCase("walk", false)]
        [TestCase("look north", false)]
        public void GetChoices_GlobalCommand_ReturnsCorrectGlobalCommand(string command, bool matches)
        {
            // Arrange
            var actions = new[] { "look around", "watch surroundings", "observe" };
            var options = actions.Select(x => new Option { Action = new Action(x) }).ToArray();
            
            var context = new ChoiceContext { Options = options };
            
            // Act
            var result = _choiceSelector.GetChoices(command, context);

            // Assert
            Assert.That(result, Is.Not.Null);
            
            var containsOption = result.Choices.Select(x => x.Option.Action.Value).Contains(command);
            Assert.That(containsOption, Is.EqualTo(matches));
        }

        [TestCase("look", 2, true)]
        [TestCase("walk", 1, true)]
        [TestCase("drink", 0, false)]
        public void GetChoices_MultipleGlobalCommands_CorrectCommandIsReturned(string command, int count, bool matches)
        {
            // Arrange
            var optionWalk = new Option { Action = new Action("walk") };
            var optionLook = new Option { Action = new Action("look") };
            
            var context = new ChoiceContext
            {
                Options = new []{ optionWalk, optionLook, optionLook }
            };
            
            // Act
            var result = _choiceSelector.GetChoices(command, context);

            // Assert
            Assert.That(result, Is.Not.Null);
            
            Assert.That(result.Choices.Count(), Is.EqualTo(count));

            var isInResultSet = result.Choices.Any(x => x.Option.Action.Value == command);
            Assert.That(isInResultSet, Is.EqualTo(matches));
        }

        [TestCase("walk west", true)]
        [TestCase("walk up", false)]
        public void GetChoices_GlobalCommandWithArguments_OnlyReturnsCommandWithValidArgument(string command, bool matches)
        {
            // Arrange
            var action = new Action("walk");
            var arguments = ModelHelper.GetArguments("north", "south", "east", "west");
            var optionWalk = new Option { Action = action, Arguments = arguments };
            
            var context = new ChoiceContext
            {
                Options = new[] { optionWalk }
            };
            
            // Act
            var result = _choiceSelector.GetChoices(command, context);
            
            // Assert
            Assert.That(result.Choices.Any(), Is.EqualTo(matches));
        }

        [TestCase("walk west", "west")]
        public void GetChoices_GlobalCommandWithArguments_ReturnsArgumentWhenMatched(string command, string argument)
        {
            // Arrange
            var action = new Action("walk");
            var arguments = ModelHelper.GetArguments("north", "south", "east", "west");
            var optionWalk = new Option { Action = action, Arguments = arguments };
            
            var context = new ChoiceContext
            {
                Options = new[] { optionWalk }
            };
            
            // Act
            var result = _choiceSelector.GetChoices(command, context);
            
            // Assert
            Assert.That(result.Choices.Select(x => x.Argument.Value), Does.Contain(argument));
        }

        [TestCase("use key on door", "use key on", "door")]
        [TestCase("use door on key", "use door on", "key")]
        public void GetChoices_InteractablesUsedTogether_ReturnsCorrectChoice(string command, string action, string target)
        {
            // Arrange
            var context = new ChoiceContext
            {
                Options = new[]
                {
                    new Option { Action = new Action("use key on"), CanHaveTarget = true },
                    new Option { Action = new Action("use door on"), CanHaveTarget = true },
                    new Option { Action = new Action("look up"), CanHaveTarget = false },
                },
                Targets = ModelHelper.GetTargets("key", "door")
            };
            
            // Act
            var result = _choiceSelector.GetChoices(command, context);
            
            // Assert
            Assert.That(result.Choices.Count(), Is.EqualTo(1));
            
            var choice = result.Choices.Single();
            Assert.That(choice.Option.Action.Value, Is.EqualTo(action));
            Assert.That(choice.Target.Value, Is.EqualTo(target));
        }

        [Test]
        public void GetChoices_InteractableExists_CannotBeTargetedWithAllOptions()
        {
            // Arrange
            var command = "walk door";
            var context = new ChoiceContext
            {
                Options = new[]
                {
                    new Option { Action = new Action("walk") }
                },
                Targets = ModelHelper.GetTargets("door")
            };
            
            // Act
            var result = _choiceSelector.GetChoices(command, context);
            
            // Assert
            Assert.IsEmpty(result.Choices);
        }
    }
}