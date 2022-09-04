namespace StoryEngine.Core.Configuration
{
    public class EngineConfiguration
    {
        public WindowSize WindowSize { get; set; } = new WindowSize();
        public string SaveGamesDirectoryPath { get; set; } = "Saves";
    }
}
