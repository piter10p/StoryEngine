namespace StoryEngine.Core
{
    public interface IGameSaver
    {
        public void SaveToFile<TData>(string fileName, TData data) where TData : class;
        public TData? LoadFromFile<TData>(string fileName) where TData : class;
        public IEnumerable<string> GetExistingFileNames();
    }
}
