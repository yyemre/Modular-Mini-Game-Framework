using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.EventSystem;
using Infrastructure.SceneManagement;

namespace Messages
{
    public class UIButtonSceneTrigger : MonoBehaviour
    {
        [SerializeField] private string sceneId;

        [Inject] private IEventBus _eventBus;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                _eventBus.Publish(new LoadSceneEvent(sceneId));
            });
        }
    }
}