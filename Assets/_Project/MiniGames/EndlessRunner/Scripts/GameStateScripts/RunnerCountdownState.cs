using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerCountdownState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;

        public RunnerCountdownState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _fsm = fsm;
            _game = game;
        }

        public void Enter()
        {
            _game.EnableCoundownScreen();
            Debug.Log("Game State: CountDown");
            Debug.Log("Countdown started...");
            _game.StartCoroutine(CountdownRoutine());
        }

        private IEnumerator CountdownRoutine()
        {
            _game.SetCountDown(3);
            yield return new WaitForSeconds(1f);
            _game.SetCountDown(2);
            yield return new WaitForSeconds(1f);
            _game.SetCountDown(1);
            yield return new WaitForSeconds(1f);
            _fsm.ChangeState(RunnerState.Playing);
        }

        public void Tick() { }

        public void Exit()
        {
            _game.DisableCoundownScreen();
        }
    }

}