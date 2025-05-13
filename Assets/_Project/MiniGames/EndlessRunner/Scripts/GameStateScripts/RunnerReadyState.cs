using System.Collections;
using Core.EventSystem;
using Core.GameStateMachine;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class RunnerReadyState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;
        private readonly IEventBus _eventBus;

        public RunnerReadyState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, IEventBus eventBus)
        {
            _fsm = fsm;
            _game = game;
            _eventBus = eventBus;
        }

        public void Enter()
        {
            _game.EnableStartScreen();
            _game.EnableReadyScreen();
            _eventBus.Publish(new PublishHighScoreEvent());
            _eventBus.Publish(new ScoreResetEvent());
            Debug.Log("Game State: Ready");
        }

        public void Tick() { }
        public void Exit()
        {
            _game.DisableStartScreen();
        }
    }

}