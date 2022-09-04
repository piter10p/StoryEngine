namespace StoryEngine.Core
{
    public interface IScenesManager
    {
        public TScene LoadScene<TScene>() where TScene : IScene;
        public void RemoveScene<TScene>() where TScene : IScene;
        public LoadedScene GetLoadedScene<TScene>() where TScene : IScene;
        public void UpdateScenes(DeltaTime deltaTime);
    }
}
