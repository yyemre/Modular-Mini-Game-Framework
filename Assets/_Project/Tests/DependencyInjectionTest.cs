using Core;
using Core.EventSystem;
using Infrastructure.AssetManagement;
using Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;

public class DependencyInjectionTest : MonoBehaviour
{
    [Inject] private IEventBus _eventBus;
    [Inject] private ISceneLoader _sceneLoader;
    [Inject] private SceneCatalog _sceneCatalog;
    [Inject] private GameManager _gameManager;

    [ContextMenu("DI Test")]
    public void DITest()
    {
        Debug.Log("=== [Dependency Injection Test] ===");

        if (_eventBus == null)
            Debug.LogError("❌ IEventBus injection FAILED");
        else
        {
            Debug.Log($"✅ IEventBus injected successfully: {_eventBus.GetType().Name}");
        }

        if (_sceneLoader == null)
            Debug.LogError("❌ ISceneLoader injection FAILED");
        else
        {
            Debug.Log($"✅ ISceneLoader injected successfully: {_sceneLoader.GetType().Name}");
        }

        if (_sceneCatalog == null)
            Debug.LogError("❌ SceneCatalog injection FAILED");
        else
        {
            Debug.Log($"✅ SceneCatalog injected: contains {_sceneCatalog.scenes.Count} scenes");
        }

        if (_gameManager == null)
            Debug.LogError("❌ GameManager injection FAILED");
        else
        {
            Debug.Log($"✅ GameManager injected successfully: {_gameManager.name}");
        }

        Debug.Log("=== [End of DI Test] ===");
    }

    
}
