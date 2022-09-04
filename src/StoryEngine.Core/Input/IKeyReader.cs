namespace StoryEngine.Core.Input
{
    public interface IKeyReader
    {
        public void Update();
        public bool KeyPressed(ConsoleKey key);
        public ConsoleKey[] GetBuffer();
    }
}
