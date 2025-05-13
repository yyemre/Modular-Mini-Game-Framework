using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerPlayingState : IMiniGameState
    {
        private readonly RunnerGame _game;
        private readonly MiniGameStateMachine<RunnerState> _fsm;

        public RunnerPlayingState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _fsm = fsm;
            _game = game;
        }

        public void Enter()
        {
            Debug.Log("Game State: Playing");
             _game.GetCharacter().EnableControl();
             _game.EnablePlayingScreen();
        }

        public void Tick() { }
        public void Exit()
        {
            _game.GetCharacter().DisableControl();
        }
    }

}