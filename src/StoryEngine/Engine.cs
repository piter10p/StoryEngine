using StoryEngine.Configuration;
using StoryEngine.Graphics;

namespace StoryEngine
{
    public class Engine
    {
        private readonly EngineConfiguration _configuration;
        private readonly IScenesManager _scenesManager;
        private readonly IWindow _window;

        public Engine(
            EngineConfiguration configuration,
            IScenesManager scenesManager,
            IWindow window)
        {
            _configuration = configuration;
            _scenesManager = scenesManager;
            _window = window;
        }

        public void Run<TEntryScene>() where TEntryScene : IScene
        {
            Initialize();
            
            _scenesManager.LoadScene<TEntryScene>();

            var lastUpdateTime = DateTime.Now;

            while (true)
            {
                var deltaTime = DateTime.Now - lastUpdateTime;
                lastUpdateTime = DateTime.Now;
                _scenesManager.UpdateScenes(new DeltaTime(deltaTime));
                _window.Display();
            }
        }

        private void Initialize()
        {
            Console.SetWindowSize(_configuration.WindowSize.Width, _configuration.WindowSize.Height);
            Console.CursorVisible = false;
        }
    }
}
