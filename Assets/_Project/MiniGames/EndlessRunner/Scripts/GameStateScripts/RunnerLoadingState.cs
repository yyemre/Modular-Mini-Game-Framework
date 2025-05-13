using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerLoadingState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;

        public RunnerLoadingState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _fsm = fsm;
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("State: Loading");
            _game.StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
            _fsm.ChangeState(RunnerState.Ready);
        }

        public void Tick() { }

        public void Exit() { }
    }


}