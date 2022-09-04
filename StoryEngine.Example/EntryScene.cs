using StoryEngine.Graphics;

namespace StoryEngine.Example
{
    public class EntryScene : IScene
    {
        private readonly IWindow _window;

        public EntryScene(IWindow window)
        {
            _window = window;
        }

        public void Initialize()
        {
        }

        public void Update(DeltaTime deltaTime)
        {
            _window.Draw(new Text("Test\nContent", new Coordinates(0, 0)));
            _window.Draw(new Text("Test2\nContent2", new Coordinates(4, 1)));
        }
    }
}
