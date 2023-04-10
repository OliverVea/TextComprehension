using TextComprehension.Level.Models;

namespace TextComprehension.Level.Interfaces
{
    public interface ISceneDescriber
    {
        SceneDescription DescribeScene(Scene scene, ObserverState observerState);
    }
}