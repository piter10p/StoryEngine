namespace StoryEngine.Graphics
{
    public interface IWindow
    {
        public void Add(Text text);
        public void Remove(Text text);
        public void Draw();
    }
}
