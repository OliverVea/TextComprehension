using TextComprehension.Level.Models;

namespace TextComprehension.Level.Interfaces
{
    public interface IObserverMover
    {
        ObserverState TurnRight(Scene scene, ObserverState observerState);
        ObserverState TurnLeft(Scene scene, ObserverState observerState);
        ObserverState MoveForward(Scene scene, ObserverState observerState);
    }
}