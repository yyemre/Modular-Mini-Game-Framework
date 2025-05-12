using System.Reflection;
using UI;
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
    [Inject] private UIPanelRegistry panelRegistry;
    [Inject] private UIManager uiManager;

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
        
        if(panelRegistry == null)
            Debug.LogError("❌ PanelRegistry injection FAILED");
        else
        {
            Debug.Log($"✅ PanelRegistry injected successfully: {panelRegistry.name}");
        }
        
        if(uiManager == null)
            Debug.LogError("❌ UiManager injection FAILED");
        else
        {
            Debug.Log($"✅ UiManager injected successfully: {uiManager.name}");
        }

        Debug.Log("=== [End of DI Test] ===");
    }
    
    [ContextMenu("Run DI Test")]
    public void RunDITest()
    {
        Debug.Log("=== [Dynamic DI Test] ===");

        var fields = GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var field in fields)
        {
            if (field.GetCustomAttribute<InjectAttribute>() == null) continue;

            var value = field.GetValue(this);
            if (value == null)
                Debug.LogError($"❌ {field.Name} injection FAILED");
            else
                Debug.Log($"✅ {field.Name} injected: {value.GetType().Name}");
        }

        Debug.Log("=== [End of DI Test] ===");
    }

    
}
