using StoryEngine.Exceptions;

namespace StoryEngine
{
    public class ScenesManager : IScenesManager
    {
        private Dictionary<Type, IScene> _scenes = new Dictionary<Type, IScene>();
        private readonly IServiceProvider _serviceProvider;

        public ScenesManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TScene LoadScene<TScene>() where TScene : IScene
        {
            var sceneType = typeof(TScene);

            if(_scenes.ContainsKey(sceneType))
                throw new SceneLoadedAlreadyException(sceneType);

            var scene = _serviceProvider.GetService(sceneType) as IScene;

            if (scene is null)
                throw new SceneNotRegisteredException(sceneType);

            scene.Initialize();
            _scenes.Add(sceneType, scene);
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

        public void UpdateScenes(DeltaTime deltaTime)
        {
            SortScenes();

            foreach (var scene in _scenes.Values)
            {
                scene.Update(deltaTime);
            }
        }

        private void SortScenes()
        {
            var scenesList = _scenes.ToList();
            scenesList.Sort((pair1, pair2) => pair2.Value.Layer.CompareTo(pair1.Value.Layer));
            _scenes = scenesList.ToDictionary(pair => pair.Key, pair => pair.Value);
        }
    }
}
