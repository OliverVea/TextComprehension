using Moq;
using TextComprehension.IO.Interfaces;
using TextComprehension.IO.Logic;
using TextComprehension.IO.Models;
using TextComprehension.IO.Test.Helpers;
using AssertionHelper = TextComprehension.IO.Test.Helpers.AssertionHelper;
using Action = TextComprehension.IO.Models.Action;

namespace TextComprehension.IO.Test.Tests
{
    public class ProvidedChoiceSelectorTests
    {
        private Mock<IChoiceSelector> _mockedChoiceSelector = null!;
        private IProvidedChoiceSelector _choiceSelector = null!;

        [SetUp]
        public void CreateChoiceSelector()
        {
            _mockedChoiceSelector = new Mock<IChoiceSelector>();
            _choiceSelector = new ProvidedChoiceSelector(_mockedChoiceSelector.Object);
        }

        [Test]
        public void GetChoices_NoProviders_EmptyResult()
        {
            // Arrange
            
            // Act
            var result = _choiceSelector.GetChoices(string.Empty);
            
            // Assert
            Assert.IsNotNull(result);
            Assert.IsEmpty(result.Choices);
        }

        [Test]
        public void AddOptionProvider_WithProvider_Succeeds()
        {
            // Arrange
            var choiceProvider = MockHelper.MockOptionProvider();

            // Act
            Assert.DoesNotThrow(() => _choiceSelector.AddOptionProvider(choiceProvider));

            // Assert
        }

        [Test]
        public void AddTargetProvider_WithProvider_Succeeds()
        {
            // Arrange
            var targetProvider = MockHelper.MockTargetProvider();

            // Act
            Assert.DoesNotThrow(() => _choiceSelector.AddTargetProvider(targetProvider));

            // Assert
        }

        [Test]
        public void GetChoices_WithProviders_UsesProviders()
        {
            // Arrange
            const string command = "build";

            var option = new Option
            {
                Action = new Action(command)
            };
            var target = ModelHelper.GetTargets("target").Single();

            var mockedOptionProvider = MockHelper.MockOptionProvider(option);
            var mockedTargetProvider = MockHelper.MockTargetProvider(target);

            _choiceSelector.AddOptionProvider(mockedOptionProvider);
            _choiceSelector.AddTargetProvider(mockedTargetProvider);

            // Act
            _choiceSelector.GetChoices(command);

            // Assert
            _mockedChoiceSelector.Verify(x => x.GetChoices(
                It.Is<string>(x => x == command),
                It.Is<ChoiceContext>(x => AssertionHelper.AssertEquals(option, x.Options.Single()) &&
                                          AssertionHelper.AssertEquals(target, x.Targets.Single()))));
        }
    }
}