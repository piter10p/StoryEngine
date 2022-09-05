namespace StoryEngine.Core.Graphics
{
    public class Text
    {
        public Text(string content, Coordinates coordinates)
        {
            _coordinates = coordinates;
            _lines = content.Split('\n').ToArray();

            var height = _lines.Length;
            var width = _lines.Max(x => x.Length);

            _bounds = new Box(
                coordinates,
                new Coordinates(coordinates.X + width, coordinates.Y + height));
        }

        private readonly string[] _lines;
        private readonly Coordinates _coordinates;
        private readonly Box _bounds;

        public char? TakeChar(Coordinates coordinates)
        {
            if (coordinates is null) throw new ArgumentNullException(nameof(coordinates));

            if (!_bounds.Includes(coordinates))
                return null;

            var x = coordinates.X - _coordinates.X;
            var y = coordinates.Y - _coordinates.Y;

            var line = _lines[y];

            if (x >= line.Length)
                return null;

            return line[x];
        }
    }
}
