using StoryEngine.Configuration;

namespace StoryEngine.Graphics
{
    public class Window : IWindow
    {
        private readonly List<Text> _texts = new List<Text>();
        private readonly EngineConfiguration _engineConfiguration;

        public Window(EngineConfiguration engineConfiguration)
        {
            _engineConfiguration = engineConfiguration;
        }

        public void Draw(Text text)
        {
            if (text is null) throw new ArgumentNullException(nameof(text));
            _texts.Add(text);
        }

        public void Display()
        {
            for (int x = 0; x < _engineConfiguration.WindowSize.Width; x++)
            {
                for (int y = 0; y < _engineConfiguration.WindowSize.Height; y++)
                {
                    var coordinates = new Coordinates(x, y);
                    char? c = null;

                    foreach (var text in _texts)
                    {
                        if(text.TakeChar(coordinates, out var ch))
                            c = ch;
                    }

                    Console.SetCursorPosition(x, y);

                    if (c is null)
                        Console.Write(' ');
                    else
                        Console.Write(c);
                }
            }

            _texts.Clear();
        }
    }
}
