using System.Collections;
using Core.EventSystem;
using Infrastructure.AssetManagement;
using UnityEngine;
using Infrastructure.SceneManagement;
using Zenject;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private ISceneLoader _loader;
        private SceneCatalog _sceneCatalog;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, SceneCatalog catalog, IEventBus eventBus)
        {
            _loader = sceneLoader;
            _sceneCatalog = catalog;
            _eventBus = eventBus;
        }
        
        private void Start()
        {
            StartCoroutine(LoadMainMenuNextFrame());
        }

        private IEnumerator LoadMainMenuNextFrame()
        {
            yield return null;
            var menuScene = _sceneCatalog.GetSceneById("MainMenu");
            _loader.LoadSceneAsync(menuScene);
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<LoadSceneEvent>(OnLoadScene);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<LoadSceneEvent>(OnLoadScene);
        }

        private void OnLoadScene(LoadSceneEvent e)
        {
            var scene = _sceneCatalog.GetSceneById(e.SceneId);
            if (scene == null)
            {
                Debug.LogError($"Scene with ID '{e.SceneId}' not found!");
                return;
            }

            _loader.LoadSceneAsync(scene);
        }
    }
}