using StoryEngine.Core.Graphics;

namespace StoryEngine.Core
{
    public class GameConsole : IGameConsole
    {
        public void Write(char c)
        {
            Console.Write(c);
        }

        public void SetCursorPosition(Coordinates coordinates)
        {
            Console.SetCursorPosition(coordinates.X, coordinates.Y);
        }

        public ConsoleKey[] GetBuffer()
        {
            var keys = new List<ConsoleKey>();

            while (Console.KeyAvailable)
            {
                keys.Add(Console.ReadKey(true).Key);
            }

            return keys.ToArray();
        }
    }
}
