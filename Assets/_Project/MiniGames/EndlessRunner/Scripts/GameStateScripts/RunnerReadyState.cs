using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerReadyState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;

        public RunnerReadyState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _fsm = fsm;
            _game = game;
        }

        public void Enter()
        {
            // UIManager.Instance.ShowStartPrompt(() =>
            // {
            //     _fsm.ChangeState(RunnerState.Countdown);
            // });
        }

        public void Tick() { }
        public void Exit()
        {
            // UIManager.Instance.HideStartPrompt();
        }
    }

}