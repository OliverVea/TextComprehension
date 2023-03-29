using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Logic
{
    public class ObserverMover : IObserverMover
    {
        public ObserverState TurnRight(Scene scene, ObserverState observerState)
        {
            return Turn(scene, observerState, 1);
        }

        public ObserverState TurnLeft(Scene scene, ObserverState observerState)
        {
            return Turn(scene, observerState, -1);
        }

        private static ObserverState Turn(Scene scene, ObserverState observerState, int change)
        {
            if (observerState.Ring == 0) return Turn(observerState, change, scene.Spurs);
            return Turn(observerState, change, 4);
        }

        private static ObserverState Turn(ObserverState observerState, int change, int directions)
        {
            return new ObserverState
            {
                Heading = (observerState.Heading + change + directions) % directions,
                Ring = observerState.Ring
            };
        }
    }
}