using FluentAssertions;
using Moq;
using StoryEngine.Core.Tests.Mocks;
using StoryEngine.Exceptions;
using System;
using Xunit;

namespace StoryEngine.Core.Tests
{
    public class SceneMockTwo : SceneMock
    {
    }

    public class ScenesManagerTests
    {
        [Fact]
        public void LoadScene_ShouldLoadScene_InitializeIt_AndReturnScene()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            var actualScene = sut.LoadScene<SceneMock>();

            //Assert
            actualScene.Should().NotBeNull();
            actualScene.Should().Be(scene);
            scene.InitializationsCounter.Should().Be(1);
        }

        [Fact]
        public void LoadScene_ThatIsLoadedAlready_ShouldThrowException()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            sut.LoadScene<SceneMock>();
            Assert.Throws<SceneLoadedAlreadyException>(() => sut.LoadScene<SceneMock>());
        }

        [Fact]
        public void LoadScene_ThatIsNotRegistered_ShouldThrowException()
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
        public void Remove_ExistingScene_ShouldSucceed()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMock>();
            sut.RemoveScene<SceneMock>();
        }

        [Fact]
        public void Remove_NotExistingScene_ShouldThrowException()
        {
            //Arrange
            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            Assert.Throws<SceneNotLoadedException>(() => sut.RemoveScene<SceneMock>());
        }

        [Fact]
        public void Remove_RemovedAlreadyScene_ShouldThrowException()
        {
            //Arrange
            var scene = new SceneMock();

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(scene);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act & Assert
            sut.LoadScene<SceneMock>();
            sut.RemoveScene<SceneMock>();
            Assert.Throws<SceneNotLoadedException>(() => sut.RemoveScene<SceneMock>());
        }

        [Fact]
        public void UpdateScenes_ShouldUpdateScenesInCorrectOrder()
        {
            //Arrange
            var sceneOne = new SceneMock();
            sceneOne.Layer = 1;

            var sceneTwo = new SceneMockTwo();
            sceneTwo.Layer = 2;

            var serviceProviderMock = new Mock<IServiceProvider>(MockBehavior.Strict);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMock)))
                .Returns(sceneOne);
            serviceProviderMock.Setup(x => x.GetService(typeof(SceneMockTwo)))
                .Returns(sceneTwo);

            var sut = new ScenesManager(serviceProviderMock.Object);

            //Act
            sut.LoadScene<SceneMockTwo>();
            sut.LoadScene<SceneMock>();

            var deltaTime = new DeltaTime(TimeSpan.FromSeconds(1));
            sut.UpdateScenes(deltaTime);

            //Assert
            sceneOne.UpdatesCounter.Should().Be(1);
            sceneTwo.UpdatesCounter.Should().Be(1);

            sceneOne.LastUpdateTime.Ticks.Should().BeLessThan(sceneTwo.LastUpdateTime.Ticks);
        }
    }
}
