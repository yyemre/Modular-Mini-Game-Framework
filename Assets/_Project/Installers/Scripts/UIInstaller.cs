using UI;
using UnityEngine;
using Zenject;

namespace _Project.Installers
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private UIPanelRegistry panelRegistry;
        [SerializeField] private UIManager uiManager;

        public override void InstallBindings()
        {
            Container.Bind<UIManager>().FromComponentInNewPrefab(uiManager).AsSingle();
            Container.Bind<UIPanelRegistry>().FromInstance(panelRegistry).AsSingle();
        }
    }

}