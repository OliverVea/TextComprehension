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

    [TestCase(0, 1, Description = "Center - Turns correctly")]
    [TestCase(7, 0, Description = "Center - Wraps around")]
    public void TurnRight_InCenter_UpdatesSpurCorrectly(int currentSpur, int expectedSpur)
    {
        // Arrange
        var state = new ObserverState
        {
            Spur = currentSpur,
            Heading = currentSpur
        };
        
        // Act
        var actualState = _observerMover.TurnRight(ExampleScene, state);

        // Assert
        Assert.That(actualState.Spur, Is.EqualTo(expectedSpur));
    }

    [TestCase(0, 0)]
    [TestCase(7, 7)]
    public void TurnRight_OnRing_SpurIsNotUpdated(int currentSpur, int expectedSpur)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = 1,
            Spur = currentSpur,
            Heading = currentSpur
        };
        
        // Act
        var actualState = _observerMover.TurnRight(ExampleScene, state);

        // Assert
        Assert.That(actualState.Spur, Is.EqualTo(expectedSpur));
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

    [TestCase(1, 0, Description = "Center - Turns correctly")]
    [TestCase(0, 7, Description = "Center - Wraps around")]
    public void TurnLeft_InCenter_UpdatesSpurCorrectly(int currentSpur, int expectedSpur)
    {
        // Arrange
        var state = new ObserverState
        {
            Spur = currentSpur,
            Heading = currentSpur
        };
        
        // Act
        var actualState = _observerMover.TurnLeft(ExampleScene, state);

        // Assert
        Assert.That(actualState.Spur, Is.EqualTo(expectedSpur));
    }

    [TestCase(0, 0)]
    [TestCase(7, 7)]
    public void TurnLeft_OnRing_SpurIsNotUpdated(int currentSpur, int expectedSpur)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = 1,
            Spur = currentSpur,
            Heading = currentSpur
        };
        
        // Act
        var actualState = _observerMover.TurnLeft(ExampleScene, state);

        // Assert
        Assert.That(actualState.Spur, Is.EqualTo(expectedSpur));
    }

    [TestCase(0, 0, 1, Description = "Center - Walks out one ring")]
    [TestCase(1, 0, 2, Description = "Ring - Walks out another ring")]
    [TestCase(2, 0, 2, Description = "Edge - Cannot walk further")]
    public void MoveForward_MovingOutwards_MovesCorrectly(int ring, int currentHeading, int expectedRing)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = ring,
            Heading = currentHeading
        };
        
        // Act
        var actualState = _observerMover.MoveForward(ExampleScene, state);

        // Assert
        Assert.That(actualState.Ring, Is.EqualTo(expectedRing));
    }

    [TestCase(0, 2, 1, Description = "Center - Walks out one ring")]
    [TestCase(1, 2, 0, Description = "Ring - Walks in to the center")]
    [TestCase(2, 2, 1, Description = "Edge - Walks towards the center")]
    public void MoveForward_MovingInwards_MovesCorrectly(int ring, int currentHeading, int expectedRing)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = ring,
            Heading = currentHeading
        };
        
        // Act
        var actualState = _observerMover.MoveForward(ExampleScene, state);

        // Assert
        Assert.That(actualState.Ring, Is.EqualTo(expectedRing));
    }
    
    [TestCase(0, 1, 1)]
    [TestCase(7, 1, 0)]
    [TestCase(1, 3, 0)]
    [TestCase(0, 3, 7)]
    public void MoveForward_MovingAlongRing_MovesCorrectly(int spur, int currentHeading, int expectedSpur)
    {
        // Arrange
        var state = new ObserverState
        {
            Ring = 1,
            Spur = spur,
            Heading = currentHeading
        };
        
        // Act
        var actualState = _observerMover.MoveForward(ExampleScene, state);

        // Assert
        Assert.That(actualState.Spur, Is.EqualTo(expectedSpur));
    }

    private static readonly Scene ExampleScene = new()
    {
        Spurs = 8,
        Rings = 2
    };
}