namespace StoryEngine.Core.Input
{
    public class Button
    {
        public Button(ConsoleKey key)
        {
            Key = key;
        }

        public ConsoleKey Key { get; }
        public KeyState State { get; set; } = KeyState.Released;
    }
}
