using StoryEngine.Core;
using StoryEngine.Core.Configuration;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Example
{
    public class EntryScene : IScene
    {
        private readonly IScenesManager _scenesManager;
        private readonly IWindow _window;
        private readonly IKeyReader _keyReader;
        private readonly int _displayHeight;

        private SceneOne? _sceneOne;
        private SceneTwo? _sceneTwo;

        public EntryScene(
            IScenesManager scenesManager,
            IWindow window,
            IKeyReader keyReader,
            EngineConfiguration configuration)
        {
            _scenesManager = scenesManager;
            _window = window;
            _keyReader = keyReader;
            _displayHeight = configuration.WindowSize.Height - 1;
        }

        public int Layer { get; } = 0;

        public void Initialize()
        {
            _sceneOne = _scenesManager.LoadScene<SceneOne>();
            _sceneTwo = _scenesManager.LoadScene<SceneTwo>();
        }

        public void Update(DeltaTime deltaTime)
        {
            _window.Draw(new Text("Press Space :)", new Coordinates(0, _displayHeight - 1)));
            _window.Draw(new Text($"Delay: {deltaTime.TimeElapsed.Milliseconds}ms.", new Coordinates(0, _displayHeight)));

            if (_keyReader.KeyPressed(ConsoleKey.Spacebar))
            {
                var sceneOneLayer = _sceneOne!.Layer;
                _sceneOne.Layer = _sceneTwo!.Layer;
                _sceneTwo.Layer = sceneOneLayer;
            }
        }
    }
}
