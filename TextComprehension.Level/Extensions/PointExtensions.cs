using System;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Extensions
{
    public static class PointExtensions
    {
        public static double GetDistance(this Point a, Point b)
        {
            var dx = a.X - b.X;
            var dy = a.Y - b.Y;
            
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2));
        }

        public static Point Subtract(this Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
    }
}