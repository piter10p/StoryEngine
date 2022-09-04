namespace StoryEngine.Core.Exceptions
{
    public class SceneNotLoadedException : Exception
    {
        public SceneNotLoadedException(Type sceneType)
            : base($"Scene not loaded: '{sceneType.FullName}'.")
        {
        }
    }
}
