using StoryEngine.Graphics;

namespace StoryEngine.Example
{
    public class SceneTwo : IScene
    {
        public int Layer { get; set; } = 2;
        private readonly IWindow _window;

        public SceneTwo(IWindow window)
        {
            _window = window;
        }

        public void Initialize()
        {
        }

        public void Update(DeltaTime deltaTime)
        {
            _window.Draw(new Text("Somesuper\nContentLong2", new Coordinates(0, 0)));
        }
    }
}
