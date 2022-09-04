using StoryEngine.Graphics;

namespace StoryEngine.Example
{
    public class SceneOne : IScene
    {
        public int Layer { get; set; } = 1;
        private readonly IWindow _window;

        public SceneOne(IWindow window)
        {
            _window = window;
        }

        public void Initialize()
        {
        }

        public void Update(DeltaTime deltaTime)
        {
            _window.Draw(new Text("Test\nContent1", new Coordinates(0, 0)));
        }
    }
}
