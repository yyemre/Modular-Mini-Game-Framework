using Core.EventSystem;
using TMPro;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class UIHighScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text highScoreText;
        [Inject] private IEventBus _eventBus;

        private void Awake()
        {
            _eventBus.Subscribe<HighScoreDisplayEvent>(OnScoreChanged);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<HighScoreDisplayEvent>(OnScoreChanged);
        }

        private void OnScoreChanged(HighScoreDisplayEvent e)
        {
            if (highScoreText != null)
                highScoreText.text = $"HighScore: {e.HighScore}";
        }
    }
}