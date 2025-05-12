using System.Collections;
using Core.EventSystem;
using Infrastructure.AssetManagement;
using UnityEngine;
using Infrastructure.SceneManagement;
using Zenject;
using UI;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        private ISceneLoader _loader;
        private SceneCatalog _sceneCatalog;
        private IEventBus _eventBus;
        private UIManager _uiManager;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, SceneCatalog catalog, IEventBus eventBus, UIManager uiManager)
        {
            _loader = sceneLoader;
            _sceneCatalog = catalog;
            _eventBus = eventBus;
            _uiManager = uiManager;
        }
        
        private void Start()
        {
            StartCoroutine(LoadMainMenuNextFrame());
        }

        private IEnumerator LoadMainMenuNextFrame()
        {
            yield return null;
            OnLoadScene(new LoadSceneEvent("MainMenu"));
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

            _loader.LoadSceneAsync(scene, () =>
            {
                _uiManager.HideAll();
                _uiManager.ShowPanel(e.SceneId);
            });
        }
    }
}