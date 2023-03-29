using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Logic;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Test.Tests;

public class ObserverMoverTests
{
    private IObserverMover _observerMover = null!;

    [SetUp]
    public void CreateChoiceSelector()
    {
        _observerMover = new ObserverMover();
    }
        
    [Test]
    public void CanInitialize()
    {
        Assert.That(_observerMover, Is.Not.Null);
    }

    [TestCase(0, 0, 1, Description = "Center - Turns correctly")]
    [TestCase(0, 7, 0, Description = "Center - Wraps around")]
    [TestCase(1, 0, 1, Description = "Ring - Turns correctly")]
    [TestCase(1, 3, 0, Description = "Ring - Wraps around")]
    public void TurnRight_VariousConfigurations_TurnsCorrectly(int ring, int currentHeading, int expectedHeading)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = ring,
            Heading = currentHeading
        };
        
        // Act
        var actualState = _observerMover.TurnRight(ExampleScene, state);

        // Assert
        Assert.That(actualState.Heading, Is.EqualTo(expectedHeading));
    }

    [TestCase(0, 1, 0, Description = "Center - Turns correctly")]
    [TestCase(0, 0, 7, Description = "Center - Wraps around")]
    [TestCase(1, 1, 0, Description = "Ring - Turns correctly")]
    [TestCase(1, 0, 3, Description = "Ring - Wraps around")]
    public void TurnLeft_VariousConfigurations_TurnsCorrectly(int ring, int currentHeading, int expectedHeading)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = ring,
            Heading = currentHeading
        };
        
        // Act
        var actualState = _observerMover.TurnLeft(ExampleScene, state);

        // Assert
        Assert.That(actualState.Heading, Is.EqualTo(expectedHeading));
    }

    private static readonly Scene ExampleScene = new()
    {
        Spurs = 8,
        Rings = 1
    };
}