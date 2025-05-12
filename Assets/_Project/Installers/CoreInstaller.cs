using Core;
using Core.EventSystem;
using Infrastructure.AssetManagement;
using Infrastructure.SceneManagement;
using UnityEngine;
using Zenject;
public class CoreInstaller : MonoInstaller
{
    [SerializeField] private SceneCatalog sceneCatalog;
    [SerializeField] private GameObject gameManagerPrefab;
    [SerializeField] private GameObject sceneLoaderPrefab;
    
    
    public override void InstallBindings()
    {
        Container.Bind<IEventBus>().To<EventBus>().AsSingle();
             
        Container.Bind<SceneCatalog>().FromInstance(sceneCatalog).AsSingle();
             
        Container.Bind<ISceneLoader>()
            .To<SceneLoader>()
            .FromComponentInNewPrefab(sceneLoaderPrefab)
            .AsSingle();
             
        Container.Bind<GameManager>()    
            .FromComponentInNewPrefab(gameManagerPrefab)
            .AsSingle()
            .NonLazy();
    }
}