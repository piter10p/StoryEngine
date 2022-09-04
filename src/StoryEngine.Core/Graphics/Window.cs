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
            for (var y = 0; y < _engineConfiguration.WindowSize.Height; y++)
            {
                var lineCoordinates = new Coordinates(0, y);
                _gameConsole.SetCursorPosition(lineCoordinates);

                for (var x = 0; x < _engineConfiguration.WindowSize.Width; x++)
                {
                    var coordinates = new Coordinates(x, y);
                    char? c = null;

                    foreach (var text in _texts)
                    {
                        if (text.TakeChar(coordinates, out var ch))
                            c = ch;
                    }

                    if (c is null)
                        _gameConsole.Write(' ');
                    else
                        _gameConsole.Write(c!.Value);
                }
            }

            _texts.Clear();
        }
    }
}
