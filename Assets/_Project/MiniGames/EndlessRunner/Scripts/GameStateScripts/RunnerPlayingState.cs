using System.Collections;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerPlayingState : IMiniGameState
    {
        private readonly RunnerGame _game;

        public RunnerPlayingState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game)
        {
            _game = game;
        }

        public void Enter()
        {
            // _game.GetRunner().EnableControl();
            // _game.GetSpawner().StartSpawning();
            // _game.GetScoreManager().StartScoring();
        }

        public void Tick() { }
        public void Exit()
        {
            // _game.GetRunner().DisableControl();
            // _game.GetSpawner().StopSpawning();
        }
    }

}