using StoryEngine.Core.Exceptions;

namespace StoryEngine.Core
{
    public class ScenesManager : IScenesManager
    {
        private Dictionary<Type, LoadedScene> _scenes = new Dictionary<Type, LoadedScene>();
        private readonly IServiceProvider _serviceProvider;

        public ScenesManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TScene LoadScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (_scenes.ContainsKey(sceneType))
                throw new SceneLoadedAlreadyException(sceneType);

            var scene = _serviceProvider.GetService(sceneType) as IScene;

            if (scene is null)
                throw new SceneNotRegisteredException(sceneType);

            scene.Initialize();
            _scenes.Add(sceneType, new LoadedScene(scene));
            SortScenes();
            return (TScene)scene;
        }

        public void RemoveScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (!_scenes.ContainsKey(sceneType))
                throw new SceneNotLoadedException(sceneType);

            _scenes.Remove(sceneType);
        }

        public LoadedScene GetLoadedScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (!_scenes.ContainsKey(sceneType))
                throw new SceneNotLoadedException(sceneType);

            return _scenes[sceneType];
        }

        public void UpdateScenes(DeltaTime deltaTime)
        {
            if (deltaTime is null) throw new ArgumentNullException(nameof(deltaTime));

            SortScenes();

            foreach (var scene in _scenes.Values)
            {
                if(scene.Enabled)
                    scene.Scene.Update(deltaTime);
            }
        }

        private void SortScenes()
        {
            var scenesList = _scenes.ToList();
            scenesList.Sort((pair1, pair2) =>
            pair2.Value.Scene.Layer.CompareTo(pair1.Value.Scene.Layer));
            _scenes = scenesList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
