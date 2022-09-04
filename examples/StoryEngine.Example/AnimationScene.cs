using StoryEngine.Core;
using StoryEngine.Core.Components.Animation;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Example
{
    public class AnimationScene : IScene
    {
        public int Layer { get; } = 0;

        private readonly IWindow _window;
        private readonly IButtonHandler _buttonHandler;

        private readonly Button _playButton = new Button(ConsoleKey.Spacebar);
        private readonly Text _text = new Text("Press spacebar.", new Coordinates(0, 2));

        private AnimationComponent? _animationComponent;

        public AnimationScene(
            IWindow window,
            IButtonHandler buttonHandler)
        {
            _window = window;
            _buttonHandler = buttonHandler;
        }

        public void Initialize()
        {
            _animationComponent = new AnimationComponent(new List<string>
                {
                    "Frame 1",
                    "Frame 2",
                    "Frame 3"
                },
                TimeSpan.FromSeconds(1),
                AnimationMode.Loop,
                new Coordinates(0, 0),
                _window);
        }

        public void Update(DeltaTime deltaTime)
        {
            _animationComponent!.Update(deltaTime);

            if(_buttonHandler.Update(_playButton))
            {
                if(_animationComponent.IsPlaying)
                    _animationComponent.Pause();
                else
                    _animationComponent.Play();
            }

            _window.Draw(_text);
        }
    }
}
