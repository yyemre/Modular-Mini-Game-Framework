using System.Collections;
using Core.EventSystem;
using Core.GameStateMachine;
using UI;
using UnityEngine;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class RunnerReadyState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;
        private readonly IEventBus _eventBus;
        private readonly UIManager _ui;

        public RunnerReadyState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, IEventBus eventBus, UIManager ui)
        {
            _fsm = fsm;
            _game = game;
            _eventBus = eventBus;
            _ui = ui;
        }

        public void Enter()
        {
            Debug.Log("State: Ready");
            _ui.ShowPanel("StartScreen");
            _ui.ShowPanel("ReadyScreen");
            
            _game.GetCharacter().ResetPosisition();
            _game.GetCharacter().ResetSpeed();
            _eventBus.Publish(new PublishHighScoreEvent());
            _eventBus.Publish(new ScoreResetEvent());
        }

        public void Tick() { }

        public void Exit()
        {
            _ui.HidePanel("StartScreen");
        }
    }



}