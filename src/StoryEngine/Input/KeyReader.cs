namespace StoryEngine.Input
{
    public class KeyReader : IKeyReader
    {
        private ConsoleKey[] _buffer = Array.Empty<ConsoleKey>();

        public void Update()
        {
            var keys = new List<ConsoleKey>();

            while(Console.KeyAvailable)
            {
                keys.Add(Console.ReadKey(true).Key);
            }

            _buffer = keys.ToArray();
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
