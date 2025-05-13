using System;
using Core.EventSystem;
using UnityEngine;
using Core.MiniGame;
using Core.GameStateMachine;
using TMPro;
using UI;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class RunnerGame : MiniGameBase<RunnerState>
    {
        [Inject] private IEventBus _eventBus;
        [Inject] private UIManager _ui;
    
        [SerializeField] private RunnerCharacterController _character;

        public RunnerCharacterController GetCharacter() => _character;

        public override void RegisterStates(MiniGameStateMachine<RunnerState> machine)
        {
            machine.RegisterState(RunnerState.Loading, new RunnerLoadingState(machine, this));
            machine.RegisterState(RunnerState.Ready, new RunnerReadyState(machine, this, _eventBus, _ui));
            machine.RegisterState(RunnerState.Countdown, new RunnerCountdownState(machine, this, _ui));
            machine.RegisterState(RunnerState.Playing, new RunnerPlayingState(machine, this, _ui));
            machine.RegisterState(RunnerState.Crashed, new RunnerCrashedState(machine, this, _eventBus));
            machine.RegisterState(RunnerState.GameOver, new RunnerGameOverState(machine, this, _eventBus, _ui));
        }

        public override void OnGameStart()
        {
            ChangeState(RunnerState.Loading);
            _eventBus.Subscribe<MiniGameStateChangeRequestEvent<RunnerState>>(e =>
            {
                _stateMachine.ChangeState(e.TargetState);
            });
        }

        public void OnDeath()
        {
            ChangeState(RunnerState.Crashed);
        }
        private void OnDestroy()
        {
            _stateMachine.UnRegisterStates();
            _eventBus.Unsubscribe<MiniGameStateChangeRequestEvent<RunnerState>>(e =>
            {
                _stateMachine.ChangeState(e.TargetState);
            });
        }
    }
}