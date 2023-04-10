using System;

namespace TextComprehension.Level.Models
{
    public class Scene
    {
        public string Description { get; set; } = "a vast empty space";
        public string? Introduction { get; set; }
        public int Spurs { get; set; }
        public int Rings { get; set; }
        public SceneObject[] SceneObjects { get; set; } = Array.Empty<SceneObject>();
    }
}