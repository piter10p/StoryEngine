namespace StoryEngine.Core.Input
{
    public class ButtonHandler : IButtonHandler
    {
        private readonly IInputReader _inputReader;

        public ButtonHandler(IInputReader inputReader)
        {
            _inputReader = inputReader;
        }

        public bool Update(Button button)
        {
            var pressed = _inputReader.IsKeyInBuffer(button.Key);

            if (pressed && button.State == KeyState.Released)
            {
                button.State = KeyState.Pressed;
                return true;
            }

            if (pressed && button.State == KeyState.Pressed)
                return false;

            button.State = KeyState.Released;
            return false;
        }
    }
}
