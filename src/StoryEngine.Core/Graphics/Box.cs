namespace StoryEngine.Graphics
{
    public class Box
    {
        public Box(Coordinates topLeft, Coordinates downRight)
        {
            TopLeft = topLeft;
            DownRight = downRight;
        }

        public Coordinates TopLeft { get; }
        public Coordinates DownRight { get; }

        public bool Includes(Coordinates position) =>
            position.X >= TopLeft.X && position.Y >= TopLeft.Y &&
            position.X < DownRight.X && position.Y < DownRight.Y;
    }
}
