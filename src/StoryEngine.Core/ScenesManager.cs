using StoryEngine.Core.Exceptions;

namespace StoryEngine.Core
{
    public class ScenesManager : IScenesManager
    {
        private Dictionary<Type, LoadedScene> _scenes = new Dictionary<Type, LoadedScene>();

        private Dictionary<Type, IScene> _scenesToLoad = new Dictionary<Type, IScene>();
        private List<Type> _scenesToRemove = new List<Type>();

        private readonly IServiceProvider _serviceProvider;

        public ScenesManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TScene? LoadScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (_scenes.ContainsKey(sceneType) || _scenesToLoad.ContainsKey(sceneType))
                return default(TScene);

            var scene = _serviceProvider.GetService(sceneType) as IScene;

            if (scene is null)
                throw new SceneNotRegisteredException(sceneType);

            _scenesToLoad.Add(sceneType, scene);
            return (TScene)scene;
        }

        public void RemoveScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (!_scenes.ContainsKey(sceneType) || _scenesToRemove.Contains(sceneType))
                return;

            _scenesToRemove.Add(sceneType);
        }

        public LoadedScene? GetLoadedScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if (!_scenes.ContainsKey(sceneType))
                return null;

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

            if (_scenesToLoad.Any())
                LoadQueuedScenes();

            if(_scenesToRemove.Any())
                RemoveQueuedScenes();
        }

        private void SortScenes()
        {
            var scenesList = _scenes.ToList();
            scenesList.Sort((pair1, pair2) =>
            pair2.Value.Scene.Layer.CompareTo(pair1.Value.Scene.Layer));
            _scenes = scenesList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private void LoadQueuedScenes()
        {
            for (var i = 0; i < _scenesToLoad.Count; i++)
            {
                var scene = _scenesToLoad.ElementAt(i);
                scene.Value.Initialize();
                _scenes.Add(scene.Key, new LoadedScene(scene.Value));
            }

            _scenesToLoad.Clear();
        }

        private void RemoveQueuedScenes()
        {
            foreach (var sceneType in _scenesToRemove)
            {
                _scenes.Remove(sceneType);
            }

            _scenesToRemove.Clear();
        }
    }
}
