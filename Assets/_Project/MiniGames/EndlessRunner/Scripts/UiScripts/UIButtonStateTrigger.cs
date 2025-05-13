using Core.EventSystem;
using Core.MiniGame;
using MiniGames.EndlessRunner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UIButtonStateTrigger : MonoBehaviour
{
    [SerializeField] private string stateId;

    [Inject] private IEventBus _eventBus;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (System.Enum.TryParse(stateId, out RunnerState parsedState))
            {
                _eventBus.Publish(new MiniGameStateChangeRequestEvent<RunnerState>(parsedState));
            }
            else
            {
                Debug.LogError($"Invalid RunnerState: {stateId}");
            }
        });
    }
}

