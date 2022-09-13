using StoryEngine.Core;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;
using StoryEngine.Core.Language;

namespace StoryEngine.Example
{
    public class LanguageScene : IScene
    {
        public int Layer { get; } = 0;

        private readonly IButtonHandler _buttonHandler;
        private readonly IWindow _window;
        private readonly ITextFileLoader _textFileLoader;

        private readonly Button _changeLanguageButton = new Button(ConsoleKey.Spacebar);
        private Text? _changeLanguageText;

        private bool _usingEnglish = true;

        public LanguageScene(
            IButtonHandler buttonHandler,
            IWindow window,
            ITextFileLoader textFileLoader)
        {
            _buttonHandler = buttonHandler;
            _window = window;
            _textFileLoader = textFileLoader;
        }

        public void Initialize()
        {
            LoadLanguage();
        }

        public void Update(DeltaTime deltaTime)
        {
            if(_buttonHandler.Update(_changeLanguageButton))
            {
                _textFileLoader.LanguageCode = GetNewLanguageCode();
                LoadLanguage();
            }

            _window.Draw(_changeLanguageText!);
        }

        private void LoadLanguage()
        {
            var textFile = _textFileLoader.LoadTextFile("test.json");
            var textContent = textFile.GetText(new TextPathBuilder().AddSegment("testElement").Build())!.Value;

            _changeLanguageText = new Text(textContent, new Coordinates(0, 0));
        }

        private string GetNewLanguageCode()
        {
            if(_usingEnglish)
            {
                _usingEnglish = false;
                return "pl";
            }

            _usingEnglish = true;
            return "en";
        }
    }
}
