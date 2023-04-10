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

    private static object[][] _testCases = 
    {
        new object[]
        {
            new ObserverState { Heading = Heading.Outward, Ring = 1, Spur = 0 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Outward, Ring = 2, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Outward, Ring = 2, Spur = 0 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Outward, Ring = 2, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Inward, Ring = 1, Spur = 0 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Inward, Ring = 0, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Inward, Ring = 0, Spur = 0 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Inward, Ring = 0, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 0 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 1 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 7 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 0 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 7 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 1 },
            Movement.Forward,
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Outward, Ring = 1, Spur = 0 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Outward, Ring = 0, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Outward, Ring = 0, Spur = 0 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Outward, Ring = 0, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Inward, Ring = 0, Spur = 0 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Inward, Ring = 1, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Inward, Ring = 2, Spur = 0 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Inward, Ring = 2, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 1 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 0 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 0 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Clockwise, Ring = 1, Spur = 7 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 0 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 1 }
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 7 },
            Movement.Backward,
            new ObserverState { Heading = Heading.Counterclockwise, Ring = 1, Spur = 0 }
        }
    };

    [TestCaseSource(nameof(_testCases))]
    public void Move_WithTestCases_ReturnsCorrectState(ObserverState initialState, Movement movement, ObserverState expectedState)
    {
        // Arrange
        
        // Act
        var actual = _observerMover.Move(Scene, initialState, movement);
        
        // Assert
        Assert.That(actual.Ring, Is.EqualTo(expectedState.Ring));
        Assert.That(actual.Spur, Is.EqualTo(expectedState.Spur));
        Assert.That(actual.Heading, Is.EqualTo(expectedState.Heading));
    }
    

    private static readonly Scene Scene = new()
    {
        Spurs = 8,
        Rings = 3
    };
}