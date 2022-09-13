namespace StoryEngine.Core.Language
{
    public class TextPathBuilder
    {
        private List<string> _segments = new List<string>();

        public TextPathBuilder AddSegment(string segment)
        {
            if (string.IsNullOrWhiteSpace(segment))
                throw new ArgumentException($"'{nameof(segment)}' cannot be null or whitespace.", nameof(segment));

            _segments.Add(segment);

            return this;
        }

        public string Build()
        {
            if (!_segments.Any())
                throw new InvalidOperationException("No segments specified.");

            return Path.Combine(_segments.ToArray());
        }
    }
}
