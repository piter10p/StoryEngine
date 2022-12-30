namespace StoryEngine.Core
{
    public interface IScenesManager
    {
        public object? LoadScene(Type sceneType);
        public void RemoveScene(Type sceneType);
        public LoadedScene? GetLoadedScene(Type sceneType);

        public TScene? LoadScene<TScene>() where TScene : IScene;
        public void RemoveScene<TScene>() where TScene : IScene;
        public LoadedScene? GetLoadedScene<TScene>() where TScene : IScene;

        public void UpdateScenes(DeltaTime deltaTime);
        public void LoadQueuedScenes();
        public void RemoveQueuedScenes();
    }
}
