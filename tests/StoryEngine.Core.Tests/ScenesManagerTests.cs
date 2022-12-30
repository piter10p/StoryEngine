using FluentAssertions;
using Moq;
using StoryEngine.Core.Exceptions;
using StoryEngine.Core.Tests.Mocks;
using System;
using Xunit;

namespace StoryEngine.Core.Tests
{
    public class SceneSecondMock : SceneMock
    {}

    public class SceneDisabledMock : SceneMock
    {}

    public class ScenesManagerTests
    {
        public DeltaTime DeltaTime => new DeltaTime(TimeSpan.Zero);

        [Fact]
        public void LoadScene_InvalidType_ShouldThrowException()
        {
            //Arrange
            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            var action = () => sut.LoadScene(typeof(string));
            action.Should().Throw<SceneTypeInvalidException>();
        }

        [Fact]
        public void RemoveScene_InvalidType_ShouldThrowException()
        {
            //Arrange
            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            var action = () => sut.RemoveScene(typeof(string));
            action.Should().Throw<SceneTypeInvalidException>();
        }

        [Fact]
        public void GetLoadedScene_InvalidType_ShouldThrowException()
        {
            //Arrange
            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            var action = () => sut.GetLoadedScene(typeof(string));
            action.Should().Throw<SceneTypeInvalidException>();
        }

        [Fact]
        public void LoadScene_ShouldEnqueueScene_NotInitialize()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            var actual = sut.LoadScene<SceneMock>();

            //Assert
            actual.Should().NotBeNull();
            actual.Should().Be(scene);
            actual!.InitializationsCounter.Should().Be(0);
            actual!.UpdatesCounter.Should().Be(0);
        }

        [Fact]
        public void LoadScene_SceneQueuedAlready_ShouldReturnNull()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            var actual = sut.LoadScene<SceneMock>();

            //Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void LoadQueuedScenes_ShouldLoadAndInitializeQueuedScene()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            sut.LoadQueuedScenes();

            //Assert
            scene.InitializationsCounter.Should().Be(1);
            scene.UpdatesCounter.Should().Be(0);
        }

        [Fact]
        public void LoadScene_SceneLoadedAlready_ShouldReturnNull()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            sut.LoadQueuedScenes();
            var actual = sut.LoadScene<SceneMock>();

            //Assert
            actual.Should().BeNull();
        }

        [Fact]
        public void LoadScene_SceneNotRegistered_ShouldThrowException()
        {
            //Arrange
            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(null);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            Assert.Throws<SceneNotRegisteredException>(() => sut.LoadScene<SceneMock>());
        }

        [Fact]
        public void RemoveScene_ShouldEnqueueScene_NotRemove()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            sut.LoadQueuedScenes();
            sut.RemoveScene<SceneMock>();
            var loadedScene = sut.GetLoadedScene<SceneMock>();

            //Assert
            loadedScene.Should().NotBeNull();
        }

        [Fact]
        public void RemoveQueuedScenes_ShouldRemoveQueuedScene()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            sut.LoadQueuedScenes();
            sut.RemoveScene<SceneMock>();
            sut.RemoveQueuedScenes();
            var loadedScene = sut.GetLoadedScene<SceneMock>();

            //Assert
            loadedScene.Should().BeNull();
        }

        [Fact]
        public void UpdateScenes_ShouldUpdateScenesInCorrectOrder_AndNotUpdateDisabledScene()
        {
            //Arrange
            var scene = new SceneMock();
            scene.Layer = 0;
            var secondScene = new SceneSecondMock();
            scene.Layer = 1;
            var disabledScene = new SceneDisabledMock();
            scene.Layer = -1;

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneSecondMock)))
                .Returns(secondScene);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneDisabledMock)))
                .Returns(disabledScene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            sut.LoadScene<SceneSecondMock>();
            sut.LoadScene<SceneDisabledMock>();
            sut.LoadQueuedScenes();

            var disabledLoadedScene = sut.GetLoadedScene<SceneDisabledMock>();
            disabledLoadedScene!.Enabled = false;

            sut.UpdateScenes(DeltaTime);

            //Arrange
            scene.UpdatesCounter.Should().Be(1);
            secondScene.UpdatesCounter.Should().Be(1);
            disabledScene.UpdatesCounter.Should().Be(0);

            scene.LastUpdateTime.Ticks.Should().BeLessThan(secondScene.LastUpdateTime.Ticks);
        }
    }
}
