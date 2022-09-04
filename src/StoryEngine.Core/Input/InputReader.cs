namespace StoryEngine.Core.Input
{
    public class InputReader : IInputReader
    {
        private ConsoleKey[] _buffer = Array.Empty<ConsoleKey>();

        private readonly IGameConsole _gameConsole;

        public InputReader(IGameConsole gameConsole)
        {
            _gameConsole = gameConsole;
        }

        public void Update()
        {
            _buffer = _gameConsole.GetBuffer();
        }

        public bool KeyPressed(ConsoleKey key)
        {
            return _buffer.Contains(key);
        }

        public ConsoleKey[] GetBuffer()
        {
            return _buffer;
        }
    }
}
