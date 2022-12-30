namespace StoryEngine.Core.Exceptions
{
    public class SceneTypeInvalidException : Exception
    {
        public SceneTypeInvalidException(Type sceneType)
            : base($"Scene type is not valid scene type: '{sceneType.FullName}'.")
        {
        }
    }
}
