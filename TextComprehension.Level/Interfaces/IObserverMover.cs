using TextComprehension.Level.Models;

namespace TextComprehension.Level.Interfaces
{
    public interface IObserverMover
    {
        ObserverState TurnRight(Scene scene, ObserverState state);
        ObserverState TurnLeft(Scene scene, ObserverState observerState);
    }
}