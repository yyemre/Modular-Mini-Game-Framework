using Core.EventSystem;
using Core.GameStateMachine;
using UI;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class RunnerGameOverState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;
        private readonly IEventBus _eventBus;
        private readonly UIManager _ui;

        public RunnerGameOverState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, IEventBus eventBus, UIManager ui)
        {
            _fsm = fsm;
            _game = game;
            _eventBus = eventBus;
            _ui = ui;
        }

        public void Enter()
        {
            Debug.Log("State: GameOver");
            _ui.ShowPanel("GameOverScreen");
        }

        public void Tick() { }

        public void Exit()
        {
            _ui.HidePanel("GameOverScreen");
            _eventBus.Publish(new ScoreChangedEvent(0));
        }
    }


}