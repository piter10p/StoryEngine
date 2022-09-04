namespace StoryEngine.Core
{
    public class LoadedScene
    {
        public LoadedScene(IScene scene)
        {
            Scene = scene;
        }

        public IScene Scene { get; }
        public bool Enabled { get; set; } = true;
    }
}
