using StoryEngine.Core;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Example
{
    public class SaveGameScene : IScene
    {
        private const string FileSaveName = "testSave";

        private readonly IWindow _window;
        private readonly IGameSaver _gameSaver;
        private readonly IButtonHandler _buttonHandler;

        private readonly Button _saveAlfaButton = new Button(ConsoleKey.A);
        private readonly Button _saveBravoButton = new Button(ConsoleKey.B);

        private readonly Text _infoText = new Text("Press A to save 'Alfa'.\nPress B to save 'Bravo'.",
            new Coordinates(0, 3));

        private Text? _valueLoadedText;

        public int Layer { get; } = 0;

        public SaveGameScene(
            IWindow window,
            IGameSaver gameSaver,
            IButtonHandler buttonHandler)
        {
            _window = window;
            _gameSaver = gameSaver;
            _buttonHandler = buttonHandler;
        }

        public void Initialize()
        {
            var save = _gameSaver.LoadFromFile<SaveData>(FileSaveName);

            var value = save is null
                ? "no value"
                : save.Value;

            _valueLoadedText = new Text($"Value loaded: '{value}'.", new Coordinates(0, 0));
        }

        public void Update(DeltaTime deltaTime)
        {
            _window.Draw(_valueLoadedText!);
            _window.Draw(_infoText);

            if(_buttonHandler.Update(_saveAlfaButton))
            {
                _gameSaver.SaveToFile(FileSaveName, new SaveData
                {
                    Value = "Alfa"
                });
            }

            if (_buttonHandler.Update(_saveBravoButton))
            {
                _gameSaver.SaveToFile(FileSaveName, new SaveData
                {
                    Value = "Bravo"
                });
            }
        }
    }
}
