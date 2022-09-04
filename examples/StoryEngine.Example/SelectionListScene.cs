using StoryEngine.Core;
using StoryEngine.Core.Components.SelectionList;
using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Example
{
    public class SelectionListScene : IScene
    {
        public int Layer { get; set; } = 0;

        private readonly IWindow _window;

        private readonly SelectionListComponent _selectionListComponent;

        private readonly ListElement _testElement1 = new ListElement("Test element 1");
        private readonly ListElement _testElement2 = new ListElement("Test element 2");
        private readonly ListElement _testElement3 = new ListElement("Test element 3");

        private readonly Text _text1 = new Text("Text 1", new Coordinates(1, 10));
        private readonly Text _text2 = new Text("Text 2", new Coordinates(1, 11));
        private readonly Text _text3 = new Text("Text 3", new Coordinates(1, 12));

        private bool _showText1 = false;
        private bool _showText2 = false;
        private bool _showText3 = false;

        public SelectionListScene(
            IButtonHandler buttonHandler,
            IWindow window)
        {
            _selectionListComponent = new SelectionListComponent(buttonHandler, window);
            _window = window;
        }

        public void Initialize()
        {
            _selectionListComponent.Coordinates = new Coordinates(5, 1);
            _selectionListComponent.AddElement(_testElement1);
            _selectionListComponent.AddElement(_testElement2);
            _selectionListComponent.AddElement(_testElement3);
            _selectionListComponent.SelectionMarker = "->";
        }

        public void Update(DeltaTime deltaTime)
        {
            var selected = _selectionListComponent.Update();

            if(selected is not null)
            {
                if (selected == _testElement1)
                    _showText1 = !_showText1;

                if (selected == _testElement2)
                    _showText2 = !_showText2;

                if (selected == _testElement3)
                    _showText3 = !_showText3;
            }

            if (_showText1)
                _window.Draw(_text1);

            if (_showText2)
                _window.Draw(_text2);

            if (_showText3)
                _window.Draw(_text3);
        }
    }
}
