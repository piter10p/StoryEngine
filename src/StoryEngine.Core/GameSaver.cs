using StoryEngine.Core.Configuration;
using System.Text.Json;

namespace StoryEngine.Core
{
    public class GameSaver : IGameSaver
    {
        private readonly string _saveGamesDirectoryPath;

        public GameSaver(EngineConfiguration engineConfiguration)
        {
            _saveGamesDirectoryPath = engineConfiguration.SaveGamesDirectoryPath;
            CreateSaveDirectoryIfNotExists();
        }

        public IEnumerable<string> GetExistingFileNames()
        {
            return Directory.GetFiles(_saveGamesDirectoryPath);
        }

        public TData? LoadFromFile<TData>(string fileName) where TData : class
        {
            var filePath = GetPath(fileName);

            if(!File.Exists(filePath))
                return null;

            using var fileStream = File.OpenRead(filePath);
            using var streamReader = new StreamReader(fileStream);

            var serialized = streamReader.ReadToEnd();
            return JsonSerializer.Deserialize<TData>(serialized)!;
        }

        public void SaveToFile<TData>(string fileName, TData data) where TData : class
        {
            var serialized = JsonSerializer.Serialize(data);
            var filePath = GetPath(fileName);

            using var fileStream = OpenFile(filePath);
            using var streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(serialized);
        }

        private string GetPath(string fileName)
        {
            return Path.Combine(_saveGamesDirectoryPath, $"{fileName}.json");
        }

        private void CreateSaveDirectoryIfNotExists()
        {
            if (!Directory.Exists(_saveGamesDirectoryPath))
                Directory.CreateDirectory(_saveGamesDirectoryPath);
        }

        private FileStream OpenFile(string filePath)
        {
            if (File.Exists(filePath))
                return File.Open(filePath, FileMode.Truncate);

            return File.Create(filePath);
        }
    }
}
