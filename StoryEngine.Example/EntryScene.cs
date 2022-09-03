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
            _window.Add(new Text("Test\nContent", new Coordinates(0, 0)));
            _window.Add(new Text("Test2\nContent2", new Coordinates(4, 1)));
        }

        public void Update(DeltaTime deltaTime)
        {
        }
    }
}
