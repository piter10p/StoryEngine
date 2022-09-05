namespace StoryEngine.Core
{
    public interface IScene
    {
        public int Layer { get; }

        public void Initialize();
        public void Update(DeltaTime deltaTime);
    }
}
