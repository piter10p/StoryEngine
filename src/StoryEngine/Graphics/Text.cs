namespace StoryEngine.Graphics
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

        public bool TakeChar(Coordinates coordinates, out char? c)
        {
            if(!_bounds.Includes(coordinates))
            {
                c = null;
                return false;
            }

            var x = coordinates.X - _coordinates.X;
            var y = coordinates.Y - _coordinates.Y;

            var line = _lines[y];

            if (x >= line.Length)
            {
                c = null;
                return false;
            }
            
            c = line[x];
            return true;
        }
    }
}
