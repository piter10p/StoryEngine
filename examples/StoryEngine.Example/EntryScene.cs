using StoryEngine.Core;
using StoryEngine.Core.Configuration;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Example
{
    public enum EnableState
    {
        AllEnabled,
        SceneOneDisabled,
        SceneTwoDisabled,
        AllDisabled
    }

    public class EntryScene : IScene
    {
        private readonly IScenesManager _scenesManager;
        private readonly IWindow _window;
        private readonly IInputReader _keyReader;
        private readonly int _displayHeight;

        private SceneOne? _sceneOne;
        private SceneTwo? _sceneTwo;

        private EnableState _enableState = EnableState.AllEnabled;

        public EntryScene(
            IScenesManager scenesManager,
            IWindow window,
            IInputReader keyReader,
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
            _window.Draw(new Text("Press Space or W :)", new Coordinates(0, _displayHeight - 1)));
            _window.Draw(new Text($"Delay: {deltaTime.TimeElapsed.Milliseconds}ms.", new Coordinates(0, _displayHeight)));

            if (_keyReader.KeyPressed(ConsoleKey.Spacebar))
            {
                SwitchScenesLayers();
            }

            if(_keyReader.KeyPressed(ConsoleKey.W))
            {
                IncrementEnableState();
                ApplyEnableState();
            }
        }

        private void SwitchScenesLayers()
        {
            var sceneOneLayer = _sceneOne!.Layer;
            _sceneOne.Layer = _sceneTwo!.Layer;
            _sceneTwo.Layer = sceneOneLayer;
        }

        private void IncrementEnableState()
        {
            if (_enableState == EnableState.AllDisabled)
                _enableState = EnableState.AllEnabled;
            else
                _enableState++;
        }

        private void ApplyEnableState()
        {
            switch(_enableState)
            {
                case EnableState.AllEnabled:
                    EnableScene<SceneOne>(true);
                    EnableScene<SceneTwo>(true);
                    break;

                case EnableState.SceneOneDisabled:
                    EnableScene<SceneOne>(false);
                    EnableScene<SceneTwo>(true);
                    break;

                case EnableState.SceneTwoDisabled:
                    EnableScene<SceneOne>(true);
                    EnableScene<SceneTwo>(false);
                    break;

                case EnableState.AllDisabled:
                    EnableScene<SceneOne>(false);
                    EnableScene<SceneTwo>(false);
                    break;
            }
        }

        private void EnableScene<TScene>(bool enabled) where TScene : IScene
        {
            var loadedScene = _scenesManager.GetLoadedScene<TScene>();
            loadedScene.Enabled = enabled;
        }
    }
}
