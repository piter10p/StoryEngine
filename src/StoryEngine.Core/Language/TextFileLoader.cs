namespace StoryEngine.Core.Language
{
    public class TextFileLoader : ITextFileLoader
    {
        public string LanguageCode { get; set; } = "en";
        private readonly ITextFileReader _textFileReader;

        private const string LangDirectoryPath = "Lang";

        public TextFileLoader(ITextFileReader textFileReader)
        {
            _textFileReader = textFileReader;
        }

        public TextFile LoadTextFile(string path)
        {
            var filePath = Path.Combine(LangDirectoryPath, LanguageCode, path);

            if(!File.Exists(filePath))
                throw new ArgumentException($"Language file not exists: '{filePath}'.", nameof(filePath));

            var jsonDocument = ReadDocument(filePath);
            return _textFileReader.ReadTextFile(jsonDocument);
        }

        private string ReadDocument(string filePath)
        {
            using var fileStream = File.OpenRead(filePath);
            using var reader = new StreamReader(fileStream);
            return reader.ReadToEnd();
        }
    }
}
