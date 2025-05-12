using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(menuName = "Game/UI Panel Registry")]
    public class UIPanelRegistry : ScriptableObject
    {
        [Serializable]
        public class PanelEntry
        {
            public string id;
            public GameObject prefab;
        }

        [SerializeField] private List<PanelEntry> panels;

        public GameObject GetPanel(string id)
            => panels.FirstOrDefault(p => p.id == id)?.prefab;
    }

}