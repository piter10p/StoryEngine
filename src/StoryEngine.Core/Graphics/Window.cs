using StoryEngine.Core.Configuration;

namespace StoryEngine.Core.Graphics
{
    public class Window : IWindow
    {
        private readonly List<Text> _texts = new List<Text>();
        private readonly EngineConfiguration _engineConfiguration;
        private readonly IGameConsole _gameConsole;

        public Window(
            EngineConfiguration engineConfiguration,
            IGameConsole gameConsole)
        {
            _engineConfiguration = engineConfiguration;
            _gameConsole = gameConsole;
        }

        public void Draw(Text text)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));
            _texts.Add(text);
        }

        public void Display()
        {
            var buffer = new char[_engineConfiguration.WindowSize.Width * _engineConfiguration.WindowSize.Height];

            for(var x = 0; x < _engineConfiguration.WindowSize.Width; x++)
            {
                for(var y = 0; y < _engineConfiguration.WindowSize.Height; y++)
                {
                    var coordinates = new Coordinates(x, y);

                    foreach (var text in _texts)
                    {
                        var c = text.TakeChar(coordinates);

                        var bufferIndex = x + y * _engineConfiguration.WindowSize.Width;

                        if (c is not null)
                            buffer[bufferIndex] = c!.Value;
                    }
                }
            }

            _gameConsole.WriteToBuffer(buffer);
            _texts.Clear();
        }
    }
}
