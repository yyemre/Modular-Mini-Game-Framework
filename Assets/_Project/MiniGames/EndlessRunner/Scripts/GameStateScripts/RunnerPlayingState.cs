using System.Collections;
using Core.GameStateMachine;
using UI;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerPlayingState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;
        private readonly UIManager _ui;

        public RunnerPlayingState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, UIManager ui)
        {
            _fsm = fsm;
            _game = game;
            _ui = ui;
        }

        public void Enter()
        {
            Debug.Log("State: Playing");
            _ui.ShowPanel("PlayingScreen");
            _game.GetCharacter().EnableControl();
        }

        public void Tick() { }

        public void Exit()
        {
            _game.GetCharacter().DisableControl();
        }
    }


}