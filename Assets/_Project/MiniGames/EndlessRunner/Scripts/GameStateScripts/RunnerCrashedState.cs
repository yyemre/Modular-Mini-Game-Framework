using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerCrashedState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;

        public RunnerCrashedState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _fsm = fsm;
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("Game State: Crashed");
            _game.GetCharacter().DisableControl();
            // _game.GetSpawner().StopSpawning();
            _game.StartCoroutine(WaitAndGoToGameOver());
        }

        private IEnumerator WaitAndGoToGameOver()
        {
            yield return new WaitForSeconds(1.5f);
            _fsm.ChangeState(RunnerState.GameOver);
        }

        public void Tick() { }
        public void Exit() { }
    }

}