using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private UIPanelRegistry panelRegistry;
        private Dictionary<string, GameObject> _activePanels = new();

        public void ShowPanel(string id)
        {
            if (_activePanels.ContainsKey(id)) return;

            var prefab = panelRegistry.GetPanel(id);
            if (prefab == null)
            {
                Debug.LogWarning($"Panel not found: {id}");
                return;
            }

            var instance = _container.InstantiatePrefab(prefab, transform);
            _activePanels[id] = instance;
        }

        public void HidePanel(string id)
        {
            if (_activePanels.TryGetValue(id, out var panel))
            {
                Destroy(panel);
                _activePanels.Remove(id);
            }
        }

        public void HideAll()
        {
            foreach (var panel in _activePanels.Values)
                Destroy(panel);

            _activePanels.Clear();
        }
    }
}