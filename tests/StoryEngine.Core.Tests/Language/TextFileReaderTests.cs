using FluentAssertions;
using StoryEngine.Core.Language;
using System.IO;
using Xunit;

namespace StoryEngine.Core.Tests.Language
{
    public class TextFileReaderTests
    {
        [Fact]
        public void ReadTextFile_ShouldReturnExpected()
        {
            //Arrange
            var sut = new TextFileReader();
            var testData = LoadTestData();

            //Act
            var textFile = sut.ReadTextFile(testData);

            //Assert
            textFile.Should().NotBeNull();
            textFile.Texts.Should().HaveCount(5);
            var texts = textFile.Texts.ToArray();
            AssertText(texts[0], "firstElement\\a", "test1");
            AssertText(texts[1], "firstElement\\b", "test2");
            AssertText(texts[2], "firstElement\\c\\d", "test3");
            AssertText(texts[3], "secondElement\\a", "test4");
            AssertText(texts[4], "secondElement\\d", "test5");
        }

        private void AssertText(Text text, string expectedPath, string expetedValue)
        {
            text.Path.Should().Be(expectedPath);
            text.Value.Should().Be(expetedValue);
        }

        private string LoadTestData()
        {
            using var stream = File.OpenRead("Language/test_file.json");
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}
