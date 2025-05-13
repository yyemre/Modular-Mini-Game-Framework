using Core.EventSystem;
using UnityEngine;
using Core.MiniGame;
using Core.GameStateMachine;
using TMPro;
using Zenject;

namespace MiniGames.EndlessRunner
{
    public class RunnerGame : MiniGameBase<RunnerState>
    {
        [Inject] private IEventBus _eventBus;
        
        [ContextMenu("Show Current State")]
        public void DebugCurrentState()
        {
            ShowCurrentState();
        }
        
        [ContextMenu("Crash")]
        public void Crash()
        {
            ChangeState(RunnerState.Crashed);
        }
        
        [SerializeField] private RunnerCharacterController character;
        
        public RunnerCharacterController GetCharacter() => character;
        
        public override void RegisterStates(MiniGameStateMachine<RunnerState> machine)
        {
            machine.RegisterState(RunnerState.Loading, new RunnerLoadingState(machine, this));
            machine.RegisterState(RunnerState.Ready, new RunnerReadyState(machine, this, _eventBus));
            machine.RegisterState(RunnerState.Countdown, new RunnerCountdownState(machine, this));
            machine.RegisterState(RunnerState.Playing, new RunnerPlayingState(machine, this));
            machine.RegisterState(RunnerState.Crashed, new RunnerCrashedState(machine, this, _eventBus));
            machine.RegisterState(RunnerState.GameOver, new RunnerGameOverState(machine, this, _eventBus));
        }

        public override void OnGameStart()
        {
            ChangeState(RunnerState.Loading);
        }

        public void OnDeath()
        {
            ChangeState(RunnerState.Crashed);
        }
        
        #region Remove // TODO: Remove use event system
        public void StartButtonPressed()
        {
            _stateMachine.ChangeState(RunnerState.Countdown);
        }
        
        public void AgainButtonPressed()
        {
            _stateMachine.ChangeState(RunnerState.Ready);
            character.ResetPosisition();
            character.ResetSpeed();
        }
        [SerializeField] GameObject startScreen;
        public void EnableStartScreen()
        {
            startScreen.SetActive(true);
        }
        public void DisableStartScreen()
        {
            startScreen.SetActive(false);
        }
        
        [SerializeField] GameObject gameOverScreen;
        public void EnableGameOverScreen()
        {
            gameOverScreen.SetActive(true);
        }
        public void DisableGameOverScreen()
        {
            gameOverScreen.SetActive(false);
        }
        
        [SerializeField] GameObject playingScreen;
        public void EnablePlayingScreen()
        {
            playingScreen.SetActive(true);
        }
        public void DisablePlayingScreen()
        {
            playingScreen.SetActive(false);
        }
        
        [SerializeField] GameObject coundownScreen;
        [SerializeField] TMP_Text countdownText;
        
        public void EnableCoundownScreen()
        {
            coundownScreen.SetActive(true);
        }
        public void DisableCoundownScreen()
        {
            coundownScreen.SetActive(false);
        }
        public void SetCountDown(int seconds)
        {
            countdownText.text = seconds.ToString();
        }
        
        [SerializeField] GameObject readyScreen;
        public void EnableReadyScreen()
        {
            readyScreen.SetActive(true);
        }
        public void DisableReadyScreen()
        {
            readyScreen.SetActive(false);
        }
        #endregion
        // public RunnerController GetRunner() => runner;
        // public ObstacleSpawner GetSpawner() => spawner;
        // public ScoreManager GetScoreManager() => scoreManager;
    }

}