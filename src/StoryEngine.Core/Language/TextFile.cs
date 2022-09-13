namespace StoryEngine.Core.Language
{
    public class TextNode
    {
        public TextNode(string path, string value)
        {
            Path = path;
            Value = value;
        }

        public string Path { get; }
        public string Value { get; }
    }

    public class TextFile
    {
        public TextFile(List<TextNode> texts)
        {
            Texts = texts;
        }

        public List<TextNode> Texts { get; }

        public TextNode? GetText(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException($"'{nameof(path)}' cannot be null or whitespace.", nameof(path));

            return Texts.FirstOrDefault(x => x.Path == path);
        }
    }
}
