using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerGameOverState : IMiniGameState
    {
        private readonly RunnerGame _game;

        public RunnerGameOverState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("Game State: GameOver");
            _game.EnableGameOverScreen();
            
            // TODO: score yazılacak
            
            // var score = _game.GetScoreManager().CurrentScore;
            // GameManager.Instance.SaveSystem.Save("runner_score", score);
            //
            // UIManager.Instance.ShowScreen<GameOverScreen>();
        }

        public void Tick() { }

        public void Exit()
        {
            _game.DisableGameOverScreen();
        }
    }

}