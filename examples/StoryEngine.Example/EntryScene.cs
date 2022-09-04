using StoryEngine.Core;
using StoryEngine.Core.Configuration;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Example
{
    public class EntryScene : IScene
    {
        public int Layer { get; } = 0;

        private readonly IWindow _window;
        private readonly IButtonHandler _buttonHandler;
        private readonly IScenesManager _scenesManager;

        private readonly Button _scenesExampleButton = new Button(ConsoleKey.A);
        private readonly Button _selectionListExampleButton = new Button(ConsoleKey.B);
        private readonly Button _saveGameExampleButton = new Button(ConsoleKey.C);
        private readonly Button _animationButton = new Button(ConsoleKey.D);

        private readonly Text Text = new Text("Press A for scenes example.\nPress B for selection list example.\nPress C for save game example.\nPress D for animation example.",
            Coordinates.Zero);

        private readonly Coordinates _delayTextCoordinates;

        private bool _enabled = true;

        public EntryScene(
            IWindow window,
            IButtonHandler buttonHandler,
            IScenesManager scenesManager,
            EngineConfiguration engineConfiguration)
        {
            _window = window;
            _buttonHandler = buttonHandler;
            _scenesManager = scenesManager;

            _delayTextCoordinates = new Coordinates(0, engineConfiguration.WindowSize.Height - 1);
        }

        public void Initialize()
        {
        }

        public void Update(DeltaTime deltaTime)
        {
            if(_enabled)
            {
                _window.Draw(Text);

                if (_buttonHandler.Update(_scenesExampleButton))
                {
                    _scenesManager.LoadScene<ScenesExampleScene>();
                    DisableCurrentSceneText();
                }

                if (_buttonHandler.Update(_selectionListExampleButton))
                {
                    _scenesManager.LoadScene<SelectionListScene>();
                    DisableCurrentSceneText();
                }

                if(_buttonHandler.Update(_saveGameExampleButton))
                {
                    _scenesManager.LoadScene<SaveGameScene>();
                    DisableCurrentSceneText();
                }

                if(_buttonHandler.Update(_animationButton))
                {
                    _scenesManager.LoadScene<AnimationScene>();
                    DisableCurrentSceneText();
                }
            }

            _window.Draw(new Text($"Delay: {deltaTime.TimeElapsed.Milliseconds}ms.", _delayTextCoordinates));
        }

        private void DisableCurrentSceneText()
        {
            _enabled = false;
        }
    }
}
