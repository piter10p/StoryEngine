using System.Text.Json;

namespace StoryEngine.Core.Language
{
    public class TextFileReader : ITextFileReader
    {
        public TextFile ReadTextFile(string textFile)
        {
            var jsonDocument = JsonDocument.Parse(textFile);

            var texts = new List<TextNode>();

            foreach (var prop in jsonDocument.RootElement.EnumerateObject())
            {
                ReadNode(prop, string.Empty, texts);
            }

            return new TextFile(texts);
        }

        private void ReadNode(JsonProperty property, string path, List<TextNode> texts)
        {
            if (property.Value.ValueKind == JsonValueKind.String)
            {
                var elementPath = Path.Combine(path, property.Name);
                texts.Add(new TextNode(elementPath, property.Value.GetString()!));
            }

            if (property.Value.ValueKind == JsonValueKind.Object)
            {
                var elementPath = Path.Combine(path, property.Name);

                foreach (var prop in property.Value.EnumerateObject())
                {
                    ReadNode(prop, elementPath, texts);
                }
            }
        }
    }
}
