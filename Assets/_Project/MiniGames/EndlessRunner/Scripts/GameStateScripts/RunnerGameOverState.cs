using Core.EventSystem;
using Core.GameStateMachine;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class RunnerGameOverState : IMiniGameState
    {
        private readonly RunnerGame _game;
        private readonly IEventBus _eventBus;

        public RunnerGameOverState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, IEventBus EventBus)
        {
            _game = game;
            _eventBus = EventBus;
        }

        public void Enter()
        {
            Debug.Log("Game State: GameOver");
            _game.EnableGameOverScreen();
        }

        public void Tick() { }

        public void Exit()
        {
            _game.DisableGameOverScreen();
            _eventBus.Publish(new ScoreChangedEvent(0));
        }
    }

}