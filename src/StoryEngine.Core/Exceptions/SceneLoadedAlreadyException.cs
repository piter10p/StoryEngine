namespace StoryEngine.Core.Exceptions
{
    public class SceneLoadedAlreadyException : Exception
    {
        public SceneLoadedAlreadyException(Type sceneType) : base($"Scene loaded already: '{sceneType.FullName}'.")
        {
        }
    }
}
