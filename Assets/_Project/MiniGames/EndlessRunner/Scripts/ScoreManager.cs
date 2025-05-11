using UnityEngine;
using TMPro;

namespace MiniGames.EndlessRunner
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;

        private int currentScore;

        public void AddScore(int value)
        {
            currentScore += value;
            UpdateUI();
        }

        public int GetScore() => currentScore;
        
        // TODO: Event system
        private void UpdateUI()
        {
            if (scoreText != null)
                scoreText.text = $"Score: {currentScore}";
        }

        public void ResetScore()
        {
            currentScore = 0;
            UpdateUI();
        }
    }
}