namespace StoryEngine.Core
{
    public class DeltaTime
    {
        public DeltaTime(TimeSpan timeElapsed)
        {
            TimeElapsed = timeElapsed;
        }

        public TimeSpan TimeElapsed { get; }
    }
}
