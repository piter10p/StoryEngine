using StoryEngine.Core.Graphics;
using StoryEngine.Core.Input;

namespace StoryEngine.Core.Components.SelectionList
{
    public class SelectionListComponent
    {
        private readonly List<ListElement> _elements = new List<ListElement>();
        private readonly IButtonHandler _buttonHandler;
        private readonly IWindow _window;

        private Button _upButton = new Button(ConsoleKey.UpArrow);
        private Button _downButton = new Button(ConsoleKey.DownArrow);
        private Button _confirmButton = new Button(ConsoleKey.Enter);

        public string SelectionMarker { get; set; } = ">";
        public int Selection { get; set; } = 0;
        public Coordinates Coordinates { get; set; } = Coordinates.Zero;

        public SelectionListComponent(
            IButtonHandler buttonHandler,
            IWindow window)
        {
            _buttonHandler = buttonHandler;
            _window = window;
        }

        public void AddElement(ListElement element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));

            _elements.Add(element);
        }

        public void RemoveElement(ListElement element)
        {
            if (element is null) throw new ArgumentNullException(nameof(element));

            if(_elements.Contains(element))
                _elements.Remove(element);
        }

        public void SetUpKey(ConsoleKey key)
        {
            _upButton = new Button(key);
        }

        public void SetDownKey(ConsoleKey key)
        {
            _downButton = new Button(key);
        }

        public ListElement? Update()
        {
            SetSelectionInListBounds();
            UpdateInput();
            DrawList();

            if (_buttonHandler.Update(_confirmButton))
            {
                return _elements.ElementAt(Selection);
            }

            return null;
        }

        private void UpdateInput()
        {
            if (_buttonHandler.Update(_upButton))
            {
                Selection--;

                if (Selection < 0)
                    Selection = _elements.Count - 1;
            }

            if (_buttonHandler.Update(_downButton))
            {
                Selection++;

                if (Selection >= _elements.Count)
                    Selection = 0;
            }
        }

        private void DrawList()
        {
            _window.Draw(new Text(SelectionMarker, new Coordinates(
                Coordinates.X - SelectionMarker.Length - 1,
                Coordinates.Y + Selection)));

            for (var i = 0; i < _elements.Count; i++)
            {
                var element = _elements.ElementAt(i);

                _window.Draw(new Text(element.Content, new Coordinates(
                    Coordinates.X,
                    Coordinates.Y + i)));
            }
        }

        private void SetSelectionInListBounds()
        {
            if(Selection < 0)
                Selection = 0;
            else if(Selection > _elements.Count - 1)
                Selection = _elements.Count - 1;
        }
    }
}
