namespace StoryEngine.Core.Language
{
    public class Text
    {
        public Text(string path, string value)
        {
            Path = path;
            Value = value;
        }

        public string Path { get; }
        public string Value { get; }
    }

    public class TextFile
    {
        public TextFile(List<Text> texts)
        {
            Texts = texts;
        }

        public List<Text> Texts { get; }
    }
}
