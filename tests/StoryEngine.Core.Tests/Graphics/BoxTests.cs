using FluentAssertions;
using StoryEngine.Core.Graphics;
using Xunit;

namespace StoryEngine.Core.Tests.Graphics
{
    public class BoxTests
    {
        public static object[] Includes_ShouldReturnExpected_Data = new object[]
        {
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(0, 0)),
                new Coordinates(0, 0),
                false
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(0, 0)),
                new Coordinates(1, 0),
                false
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(0, 0)),
                new Coordinates(0, 1),
                false
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(0, 0)),
                new Coordinates(1, 1),
                false
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(1, 1)),
                new Coordinates(0, 0),
                true
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(1, 1)),
                new Coordinates(1, 0),
                false
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(1, 1)),
                new Coordinates(0, 1),
                false
            },
            new object[]
            {
                new Box(new Coordinates(0, 0), new Coordinates(1, 1)),
                new Coordinates(1, 1),
                false
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(0, 0),
                false
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(0, 1),
                false
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(1, 0),
                false
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(1, 1),
                true
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(2, 2),
                true
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(3, 2),
                false
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(2, 3),
                false
            },
            new object[]
            {
                new Box(new Coordinates(1, 1), new Coordinates(3, 3)),
                new Coordinates(3, 3),
                false
            },
        };

        [Theory]
        [MemberData(nameof(Includes_ShouldReturnExpected_Data))]
        public void Includes_ShouldReturnExpected(Box box, Coordinates coordinates, bool expected)
        {
            var actual = box.Includes(coordinates);
            actual.Should().Be(expected);
        }
    }
}
