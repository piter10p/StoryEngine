namespace StoryEngine.Core.Exceptions
{
    public class SceneNotRegisteredException : Exception
    {
        public SceneNotRegisteredException(Type sceneType)
            : base($"Scene '{sceneType.FullName}' not registered in DI container.")
        {
        }
    }
}
