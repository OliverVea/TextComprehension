using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Logic;

namespace TextComprehension.Level.Test;

public class ObserverMoverTests
{
    private IObserverTester _observerTester = null!;

    [SetUp]
    public void CreateChoiceSelector()
    {
        _observerTester = new ObserverTester();
    }
        
    [Test]
    public void CanInitialize()
    {
        Assert.That(_observerTester, Is.Not.Null);
    }
}