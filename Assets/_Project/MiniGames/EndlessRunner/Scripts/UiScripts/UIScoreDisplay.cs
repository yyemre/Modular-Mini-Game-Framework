using Core.EventSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class UIScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [Inject] private IEventBus _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<ScoreChangedEvent>(OnScoreChanged);
        }

        private void OnScoreChanged(ScoreChangedEvent e)
        {
            if (scoreText != null)
                scoreText.text = $"Score: {e.Score}";
        }
    }
}