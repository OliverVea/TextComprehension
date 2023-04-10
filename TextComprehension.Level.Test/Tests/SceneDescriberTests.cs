using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Logic;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Test.Tests;

public class SceneDescriberTests
{
    private ISceneDescriber _sceneDescriber = null!;
    private static readonly Scene Scene = new()
    {
        Description = "a lush garden",
        Rings = 1,
        Spurs = 2,
        SceneObjects = new[]
        {
            new SceneObject
            {
                Description = "a %ared apple",
                Position = new Point(0.0, 0.0)
            },
            new SceneObject
            {
                Description = "a %awhite t-shirt on the floor",
                Position = new Point(0.0, -0.5)
            },
            new SceneObject
            {
                Description = "a %alarge flowing river",
                Position = new Point(0.0, -5.0)
            }
        }
    };

    [SetUp]
    public void CreateChoiceSelector()
    {
        _sceneDescriber = new SceneDescriber();
    }

    [TestCaseSource(nameof(_timeOfDay))]
    public void DescribeScene_DifferentTimesOfDay_DescribesCorrectly(double timeOfDay, string expectedDescription)
    {
        // Arrange
        var state = new ObserverState
        {
            TimeOfDay = timeOfDay,
            Heading = Heading.Outward
        };
        
        // Act
        var actual = _sceneDescriber.DescribeScene(Scene, state);
        
        // Assert
        Assert.That(actual.Description, Is.EqualTo(expectedDescription));
    }

    [TestCaseSource(nameof(_sceneObjects))]
    public void DescribeScene_DifferentObserverStates_DescribesSceneObjectsCorrectly(ObserverState observerState,
        string expectedDescription)
    {
        // Act
        var actual = _sceneDescriber.DescribeScene(Scene, observerState);
        
        // Assert
        Assert.That(actual.Description, Is.EqualTo(expectedDescription));
    }

    private static object[][] _sceneObjects =
    {
        new object[]
        {
            new ObserverState { Heading = Heading.Inward, Spur = 0, Ring = 0 },
            "a lush garden at noon with a nearby red apple and behind it a nearby white t-shirt on the floor in the distance there is a large flowing river"
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Inward, Spur = 1, Ring = 0 },
            "a lush garden at noon with a nearby white t-shirt on the floor and behind it a nearby red apple"
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Outward, Spur = 1, Ring = 0 },
            "a lush garden at noon in the distance there is a large flowing river"
        },
        new object[]
        {
            new ObserverState { Heading = Heading.Clockwise, Spur = 0, Ring = 0 },
            "a lush garden at noon"
        },
    };

    private static object[][] _timeOfDay =
    {
        new object[]
        {
            0.0,
            "a lush garden at night"
        },
        new object[]
        {
            0.25,
            "a lush garden in the morning"
        },
        new object[]
        {
            0.5,
            "a lush garden at noon"
        },
        new object[]
        {
            0.75,
            "a lush garden in the evening"
        },
        new object[]
        {
            1.0,
            "a lush garden at night"
        },
    };
}