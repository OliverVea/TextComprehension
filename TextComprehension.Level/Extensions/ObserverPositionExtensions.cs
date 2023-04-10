using System;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Extensions
{
    public static class ObserverPositionExtensions
    {
        public static Position GetPosition(this ObserverState observerState, Scene scene)
        {
            var spurAngle = 2 * Math.PI * observerState.Spur / scene.Spurs;
            var d = observerState.Ring + 1;
            var x = Math.Sin(spurAngle) * d;
            var y = Math.Cos(spurAngle) * d;
            var headingAngle = ((double)observerState.Heading / 4 * 2 * Math.PI) + spurAngle;

            return new Position
            {
                X = x,
                Y = y,
                Angle = headingAngle
            };
        }
    }
}