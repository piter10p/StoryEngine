namespace StoryEngine.Core.Language
{
    public interface ITextFileLoader
    {
        public string LanguageCode { get; set; }
        public TextFile LoadTextFile(string path);
    }
}
