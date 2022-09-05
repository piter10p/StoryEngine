using StoryEngine.Core.Graphics;

namespace StoryEngine.Core
{
    public interface IGameConsole
    {
        public void WriteToBuffer(char[] content);
        public ConsoleKey[] GetInput();
    }
}
