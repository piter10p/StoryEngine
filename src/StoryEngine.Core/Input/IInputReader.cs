namespace StoryEngine.Core.Input
{
    public interface IInputReader
    {
        public void Update();
        public bool IsKeyInBuffer(ConsoleKey key);
        public ConsoleKey[] GetBuffer();
    }
}
