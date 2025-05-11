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
            // UIManager.Instance.ShowCountdown(() =>
            // {
            //     _fsm.ChangeState(RunnerState.Playing);
            // });
        }

        public void Tick() { }
        public void Exit() { }
    }

}