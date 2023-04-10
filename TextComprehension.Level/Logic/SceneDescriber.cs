using System;
using System.Collections.Generic;
using System.Linq;
using TextComprehension.Level.Extensions;
using TextComprehension.Level.Interfaces;
using TextComprehension.Level.Models;

namespace TextComprehension.Level.Logic
{
    public class SceneDescriber : ISceneDescriber
    {
        public SceneDescription DescribeScene(Scene scene, ObserverState observerState)
        {
            var sceneDescriptionComponents = GetSceneDescriptionComponents(scene, observerState);
            var sceneDescription = string.Join(' ', sceneDescriptionComponents);
            
            return new SceneDescription
            {
                Description = sceneDescription
            };
        }

        private IEnumerable<string> GetSceneDescriptionComponents(Scene scene, ObserverState observerState)
        {
            var components = new List<string> { scene.Description };

            var timeOfDay = ConvertTimeOfDay(observerState);
            components.Add(timeOfDay);
            
            var sceneObjectDescriptions = GetSceneObjectDescriptions(scene, observerState);
            components.AddRange(sceneObjectDescriptions);

            return components;
        }

        private string ConvertTimeOfDay(ObserverState observerState)
        {
            if (observerState.TimeOfDay <= 0.125) return "at night";
            if (observerState.TimeOfDay <= 0.375) return "in the morning";
            if (observerState.TimeOfDay <= 0.625) return "at noon";
            if (observerState.TimeOfDay <= 0.875) return "in the evening";
            return "at night";
        }

        private IEnumerable<string> GetSceneObjectDescriptions(Scene scene, ObserverState observerState)
        {
            var descriptions = new List<string>();
            
            var nearObjectDescriptions = GetNearObjectDescriptions(scene, observerState);
            if (nearObjectDescriptions != "") descriptions.Add(nearObjectDescriptions);
            
            var farObjectDescriptions = GetFarObjectDescriptions(scene, observerState);
            if (farObjectDescriptions != "") descriptions.Add(farObjectDescriptions);

            return descriptions;
        }

        private string GetNearObjectDescriptions(Scene scene, ObserverState observerState)
        {
            var nearObjects = scene.SceneObjects
                .Where(o => IsVisible(o, scene, observerState))
                .Where(o => IsNear(o, scene, observerState))
                .OrderBy(o => observerState.GetPosition(scene).GetDistance(o.Position))
                .Select(o => GetObjectWithAdjective(o.Description, "nearby "))
                .ToArray();

            if (!nearObjects.Any()) return "";
            
            return "with " + string.Join(" and behind it ", nearObjects);
        }

        private string GetFarObjectDescriptions(Scene scene, ObserverState observerState)
        {
            var farObjects = scene.SceneObjects
                .Where(o => IsVisible(o, scene, observerState))
                .Where(o => !IsNear(o, scene, observerState))
                .OrderBy(o => observerState.GetPosition(scene).GetDistance(o.Position))
                .Select(o => GetObjectWithAdjective(o.Description, ""))
                .ToArray();

            if (!farObjects.Any()) return "";

            return "in the distance there is " + string.Join(" and behind it ", farObjects);
        }

        private bool IsVisible(SceneObject sceneObject, Scene scene, ObserverState observerState)
        {
            const double fov = Math.PI / 2;

            var observerPosition = observerState.GetPosition(scene);
            var sceneObjectRelativeToObserver = sceneObject.Position.Subtract(observerPosition);

            // Axes are flipped on purpose.
            var angle = Math.Atan2(sceneObjectRelativeToObserver.X, sceneObjectRelativeToObserver.Y);
            angle = SubtractAngle(angle, observerPosition.Angle);
            
            return angle >= -fov / 2 && angle <= fov / 2;
        }

        private double SubtractAngle(double a, double b)
        {
            var angle = a - b;
            while (angle < -Math.PI) angle += 2 * Math.PI;
            while (angle > Math.PI) angle -= 2 * Math.PI;
            return angle;
        }

        private bool IsNear(SceneObject sceneObject, Scene scene, ObserverState observerState)
        {
            var observerPosition = observerState.GetPosition(scene);
            var distance = observerPosition.GetDistance(sceneObject.Position);
            return distance <= 1.5;
        }


        private string GetObjectWithAdjective(string sceneObject, string adjective)
        {
            return sceneObject.Replace("%a", adjective);
        }
    }
}