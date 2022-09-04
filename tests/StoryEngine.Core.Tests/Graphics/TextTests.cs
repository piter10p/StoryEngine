using FluentAssertions;
using StoryEngine.Graphics;
using Xunit;

namespace StoryEngine.Core.Tests.Graphics
{
    public class TextTests
    {
        public static object[] TakeChar_ShouldReturnExpected_Data = new object[]
        {
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(2, 1),
                true,
                'e'
            },
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(1, 2),
                true,
                'S'
            },
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(0, 1),
                false,
                null
            },
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(11, 1),
                true,
                't'
            },
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(12, 1),
                false,
                null
            },
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(8, 2),
                true,
                'a'
            },
            new object?[]
            {
                new Text("TestContent\nSomeData", new Coordinates(1, 1)),
                new Coordinates(9, 2),
                false,
                null
            },
        };

        [Theory]
        [MemberData(nameof(TakeChar_ShouldReturnExpected_Data))]
        public void TakeChar_ShouldReturnExpected(
            Text text, Coordinates coordinates, bool expeted, char? expectedChar)
        {
            var actual = text.TakeChar(coordinates, out var actualChar);

            actual.Should().Be(expeted);
            actualChar.Should().Be(expectedChar);
        }
    }
}
