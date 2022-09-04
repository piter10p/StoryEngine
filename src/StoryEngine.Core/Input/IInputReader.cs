namespace StoryEngine.Core.Input
{
    public interface IInputReader
    {
        public void Update();
        public bool KeyPressed(ConsoleKey key);
        public ConsoleKey[] GetBuffer();
    }
}
