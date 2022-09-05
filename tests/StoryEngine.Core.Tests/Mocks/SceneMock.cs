using System;

namespace StoryEngine.Core.Tests.Mocks
{
    public class SceneMock : IScene
    {
        public int Layer { get; set; } = 0;

        public int InitializationsCounter { get; private set; } = 0;
        public DateTime LastInitializationTime { get; private set; } = DateTime.Now;

        public int UpdatesCounter { get; private set; } = 0;
        public DateTime LastUpdateTime { get; private set; } = DateTime.Now;

        public void Initialize()
        {
            InitializationsCounter = InitializationsCounter + 1;
        }

        public void Update(DeltaTime deltaTime)
        {
            UpdatesCounter = UpdatesCounter + 1;
        }
    }
}
