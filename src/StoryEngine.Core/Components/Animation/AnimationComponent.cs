using StoryEngine.Core.Graphics;

namespace StoryEngine.Core.Components.Animation
{
    public class AnimationComponent
    {
        private readonly List<string> _frames;
        private readonly TimeSpan _delay;
        private readonly AnimationMode _mode;
        private readonly Coordinates _coordinates;
        private readonly IWindow _window;

        public bool IsPlaying { get; private set; } = false;
        public bool PlayingEnded { get; private set; } = false;
        public int CurrentFrame { get; private set; } = 0;

        private TimeSpan _timeFromLastUpdate = TimeSpan.Zero;

        public AnimationComponent(
            List<string> frames,
            TimeSpan delay,
            AnimationMode mode,
            Coordinates coordinates,
            IWindow window)
        {
            _frames = frames;
            _delay = delay;
            _mode = mode;
            _coordinates = coordinates;
            _window = window;
        }

        public void Play()
        {
            IsPlaying = true;
            _timeFromLastUpdate = TimeSpan.Zero;

            if(PlayingEnded)
            {
                CurrentFrame = 0;
                PlayingEnded = false;
            }
        }

        public void Pause()
        {
            IsPlaying = false;
        }

        public void Stop()
        {
            IsPlaying = false;
            PlayingEnded = true;
        }

        public void Update(DeltaTime deltaTime)
        {
            if(IsPlaying)
            {
                _timeFromLastUpdate += deltaTime.TimeElapsed;

                if (_timeFromLastUpdate > _delay)
                {
                    CurrentFrame++;

                    if (CurrentFrame >= _frames.Count)
                        HandleAnimationEnd();

                    _timeFromLastUpdate = TimeSpan.Zero;
                }
            }

            _window.Draw(new Text(_frames.ElementAt(CurrentFrame), _coordinates));
        }

        private void HandleAnimationEnd()
        {
            switch(_mode)
            {
                case AnimationMode.Normal:
                    CurrentFrame = _frames.Count - 1;
                    Stop();
                    break;

                case AnimationMode.Loop:
                    CurrentFrame = 0;
                    Play();
                    break;
            }
        }
    }
}
