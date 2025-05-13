using System;
using Core.EventSystem;
using UnityEngine;
using Zenject;
using Infrastructure.SaveSystem;

namespace MiniGames.EndlessRunner
{
    public class ScoreManager : MonoBehaviour
    {
        [Inject] private IEventBus _eventBus;
        [Inject] private ISaveSystem _saveSystem;

        private int currentScore;

        private void OnEnable()
        {
            _eventBus.Subscribe<ScoreAddingEvent>(AddScore);
            _eventBus.Subscribe<ScoreResetEvent>(ResetScore);
            _eventBus.Subscribe<ScoreSavingEvent>(SaveScore);
            _eventBus.Subscribe<PublishHighScoreEvent>(PublishHighScore);
        }

        public void AddScore(ScoreAddingEvent e)
        {
            currentScore += e.PointToAdd;
            PublishScore();
        }

        public int GetScore() => currentScore;

        public void ResetScore(ScoreResetEvent e)
        {
            currentScore = 0;
            PublishScore();
        }

        private void PublishScore()
        {
            _eventBus.Publish(new ScoreChangedEvent(currentScore));
        }

        private void SaveScore(ScoreSavingEvent e)
        {
            var save = _saveSystem.Load();
            if (save.runnerData.highScore < currentScore)
            {
                save.runnerData.highScore = currentScore;
                _saveSystem.Save(save);
            }
            
            PublishHighScore(null);
        }

        private void PublishHighScore(PublishHighScoreEvent e)
        {
            var save = _saveSystem.Load();
            _eventBus.Publish(new HighScoreDisplayEvent(save.runnerData.highScore));
        }
        
    }
}