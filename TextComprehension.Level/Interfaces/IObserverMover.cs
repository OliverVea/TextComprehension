using TextComprehension.Level.Models;

namespace TextComprehension.Level.Interfaces
{
    public interface IObserverMover
    {
        ObserverState Move(Scene scene, ObserverState observerState, Movement movement);
    }
}