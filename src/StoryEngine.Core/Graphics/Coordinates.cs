namespace StoryEngine.Graphics
{
    public class Coordinates
    {
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public static Coordinates Zero => new Coordinates(0, 0);
    }
}
