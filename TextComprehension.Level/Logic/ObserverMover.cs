using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Logic
{
    public class ObserverMover : IObserverMover
    {
        public ObserverState Move(Scene scene, ObserverState observerState, Movement movement)
        {
            var ring = GetRing(scene, observerState, movement);
            var spur = GetSpur(scene, observerState, movement);
            var heading = GetHeading(observerState, movement);
            
            return new ObserverState
            {
                Ring = ring,
                Spur = spur,
                Heading = heading,
            };
        }

        private static int GetRing(Scene scene, ObserverState observerState, Movement movement)
        {
            var change = GetChange((int)movement, (int)Movement.Forward, (int)Movement.Backward);
            change *= GetChange((int)observerState.Heading, (int)Heading.Outward, (int)Heading.Inward);

            var newRing = observerState.Ring + change;

            if (0 <= newRing && newRing < scene.Rings) return newRing;
            return observerState.Ring;
        }

        private static int GetSpur(Scene scene, ObserverState observerState, Movement movement)
        {
            var change = GetChange((int)movement, (int)Movement.Forward, (int)Movement.Backward);
            change *= GetChange((int)observerState.Heading, (int)Heading.Clockwise, (int)Heading.Counterclockwise);

            return (observerState.Spur + change + scene.Spurs) % scene.Spurs;
        }

        private static Heading GetHeading(ObserverState observerState, Movement movement)
        {
            var change = GetChange((int)movement, (int)Movement.TurnRight, (int)Movement.TurnLeft);

            var heading = ((int)observerState.Heading + change + 4) % 4;
            return (Heading)heading;
        }

        private static int GetChange(int value, int positive, int negative)
        {
            if (value == positive) return 1;
            if (value == negative) return -1;
            return 0;
        }
    }
}