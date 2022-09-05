namespace StoryEngine.Core.Exceptions
{
    public class SceneNotRegisteredException : Exception
    {
        public SceneNotRegisteredException(Type sceneType)
            : base($"Scene not registered in DI container: '{sceneType.FullName}'.")
        {
        }
    }
}
