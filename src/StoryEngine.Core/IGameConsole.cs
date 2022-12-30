namespace StoryEngine.Core
{
    public interface IGameConsole
    {
        public void Initialize(int width, int height);
        public void WriteToBuffer(char[] content);
        public ConsoleKey[] GetInput();
    }
}
