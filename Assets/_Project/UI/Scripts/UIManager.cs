using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [Inject] private DiContainer _container;
        [Inject] private UIPanelRegistry panelRegistry;
        private Dictionary<string, GameObject> _activePanels = new();
        
        [SerializeField] private List<TextBinding> textBindings;
        private Dictionary<string, TMP_Text> _textMap;

        private void Awake()
        {
            _textMap = new Dictionary<string, TMP_Text>();
            foreach (var bind in textBindings)
                _textMap[bind.key] = bind.text;
        }
        
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

        public void UpdateText(string key, string value)
        {
            if (_textMap.TryGetValue(key, out var text))
                text.text = value;
            else
                Debug.LogWarning($"UIManager: No text bound for key '{key}'");
        }
    }
}