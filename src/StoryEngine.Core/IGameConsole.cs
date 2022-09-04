using StoryEngine.Core.Graphics;

namespace StoryEngine.Core
{
    public interface IGameConsole
    {
        public void Write(char c);
        public void SetCursorPosition(Coordinates coordinates);
        public ConsoleKey[] GetBuffer();
    }
}
