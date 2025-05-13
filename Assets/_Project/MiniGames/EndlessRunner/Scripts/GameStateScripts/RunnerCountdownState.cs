using System.Collections;
using Core.GameStateMachine;
using UI;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerCountdownState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;
        private readonly UIManager _ui;

        public RunnerCountdownState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, UIManager ui)
        {
            _fsm = fsm;
            _game = game;
            _ui = ui;
        }

        public void Enter()
        {
            Debug.Log("State: Countdown");
            _ui.ShowPanel("CountdownScreen");
            _game.StartCoroutine(CountdownRoutine());
        }

        private IEnumerator CountdownRoutine()
        {
            for (int i = 3; i > 0; i--)
            {
                // _ui.UpdateText("CountdownText", i.ToString());
                yield return new WaitForSeconds(1f);
            }

            _fsm.ChangeState(RunnerState.Playing);
        }

        public void Tick() { }

        public void Exit()
        {
            _ui.HidePanel("CountdownScreen");
        }
    }


}