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
            if (observerState.Ring == 0) return Turn(observerState, change, scene.Spurs, change);
            return Turn(observerState, change, 4, 0);
        }

        private static ObserverState Turn(ObserverState observerState, int change, int directions, int spurChange)
        {
            var heading = (observerState.Heading + change + directions) % directions;
            
            var spur = observerState.Spur;
            if (spurChange != 0) spur = (spur + spurChange + directions) % directions;
            
            return new ObserverState
            {
                Heading = heading,
                Ring = observerState.Ring,
                Spur = spur
            };
        }
        
        

        public ObserverState MoveForward(Scene scene, ObserverState observerState)
        {
            return Move(scene, observerState, 1);
        }

        private static ObserverState Move(Scene scene, ObserverState observerState, int change)
        {
            var ringChange = GetRingChange(observerState, change);
            var spurChange = GetSpurChange(observerState, change);
            
            if (InvalidMovement(scene, observerState, ringChange))
                return observerState;

            return new ObserverState
            {
                Heading = observerState.Heading,
                Ring = observerState.Ring + ringChange,
                Spur = (observerState.Spur + spurChange + scene.Spurs) % scene.Spurs
            };
        }

        private static int GetSpurChange(ObserverState observerState, int change)
        {
            if (observerState.Ring == 0) return 0;
            
            switch (observerState.Heading)
            {
                case 1: return 1 * change;
                case 3: return -1 * change;
            }

            return 0;
        }

        private static bool InvalidMovement(Scene scene, ObserverState observerState, int change)
        {
            return observerState.Ring + change > scene.Rings;
        }

        private static int GetRingChange(ObserverState observerState, int change)
        {
            if (observerState.Ring == 0) return change;
            
            switch (observerState.Heading)
            {
                case 0: return 1 * change;
                case 2: return -1 * change;
            }

            return 0;
        }
    }
}