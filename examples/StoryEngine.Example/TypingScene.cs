using StoryEngine.Core;
using StoryEngine.Core.Components.Typer;
using StoryEngine.Core.Graphics;

namespace StoryEngine.Example
{
    public class TypingScene : IScene
    {
        private const string Text = "Some longer text\njust for testing stuff :)";

        public int Layer { get; } = 0;

        private readonly IWindow _window;

        private TyperComponent? _typerComponent;

        public TypingScene(IWindow window)
        {
            _window = window;
        }

        public void Initialize()
        {
            _typerComponent = new TyperComponent(Text, TimeSpan.FromMilliseconds(50), _window);
            _typerComponent.Play();
        }

        public void Update(DeltaTime deltaTime)
        {
            _typerComponent!.Update(deltaTime);
        }
    }
}
