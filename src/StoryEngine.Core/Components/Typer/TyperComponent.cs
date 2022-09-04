using StoryEngine.Core.Graphics;

namespace StoryEngine.Core.Components.Typer
{
    public class TyperComponent
    {
        private readonly string _content;
        private readonly TimeSpan _delay;
        private readonly IWindow _window;

        public Coordinates Coordinates { get; set; } = Coordinates.Zero;

        public bool IsPlaying { get; private set; }

        private int _currentPosition = 0;
        private TimeSpan _timeFromLastUpdate = TimeSpan.Zero;

        public TyperComponent(
            string content,
            TimeSpan delay,
            IWindow window)
        {
            _content = content;
            _delay = delay;
            _window = window;
        }

        public void Play()
        {
            IsPlaying = true;
            _currentPosition = 0;
            _timeFromLastUpdate = TimeSpan.Zero;
        }

        public void Update(DeltaTime deltaTime)
        {
            if(IsPlaying)
            {
                _timeFromLastUpdate += deltaTime.TimeElapsed;

                if(_timeFromLastUpdate > _delay)
                {
                    if(_currentPosition == _content.Length - 1)
                        IsPlaying = false;

                    _currentPosition++;
                }
            }

            var text = new Text(_content.Substring(0, _currentPosition), Coordinates);
            _window.Draw(text);
        }
    }
}
