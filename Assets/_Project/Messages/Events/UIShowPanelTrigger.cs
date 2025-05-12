using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UIShowPanelTrigger : MonoBehaviour
    {
        [SerializeField] private string panelId;
        [Inject] private UIManager _uiManager;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
                _uiManager.ShowPanel(panelId));
        }
    }

}