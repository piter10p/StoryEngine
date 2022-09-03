using StoryEngine.Exceptions;

namespace StoryEngine
{
    public class ScenesManager : IScenesManager
    {
        private readonly Dictionary<Type, IScene> _scenes = new Dictionary<Type, IScene>();
        private readonly IServiceProvider _serviceProvider;

        public ScenesManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void LoadScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if(_scenes.ContainsKey(sceneType))
                throw new SceneLoadedAlreadyException(sceneType);

            var scene = _serviceProvider.GetService(sceneType) as IScene;

            if (scene is null)
                throw new SceneNotRegisteredException(sceneType);

            scene.Initialize();
            _scenes.Add(sceneType, scene);
        }

        public void RemoveScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (!_scenes.ContainsKey(sceneType))
                throw new SceneNotLoadedException(sceneType);

            _scenes.Remove(sceneType);
        }

        public void UpdateScenes(DeltaTime deltaTime)
        {
            foreach (var scene in _scenes.Values)
            {
                scene.Update(deltaTime);
            }
        }
    }
}
