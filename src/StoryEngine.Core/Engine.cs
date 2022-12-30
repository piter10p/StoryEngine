using StoryEngine.Core.Configuration;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Core
{
    public class Engine
    {
        private readonly EngineConfiguration _configuration;
        private readonly IScenesManager _scenesManager;
        private readonly IInputReader _inputReader;
        private readonly IWindow _window;
        private readonly IGameConsole _gameConsole;

        public Engine(
            EngineConfiguration configuration,
            IScenesManager scenesManager,
            IInputReader inputReader,
            IWindow window,
            IGameConsole gameConsole)
        {
            _configuration = configuration;
            _scenesManager = scenesManager;
            _inputReader = inputReader;
            _window = window;
            _gameConsole = gameConsole;
        }

        public void Run<TEntryScene>() where TEntryScene : IScene
        {
            _gameConsole.Initialize(
                _configuration.WindowSize.Width,
                _configuration.WindowSize.Height);

            _scenesManager.LoadScene<TEntryScene>();

            var lastUpdateTime = DateTime.Now;

            while (true)
            {
                var deltaTime = DateTime.Now - lastUpdateTime;
                lastUpdateTime = DateTime.Now;
                _inputReader.Update();
                _scenesManager.UpdateScenes(new DeltaTime(deltaTime));
                _scenesManager.RemoveQueuedScenes();
                _scenesManager.LoadQueuedScenes();
                _window.Display();
            }
        }
    }
}
