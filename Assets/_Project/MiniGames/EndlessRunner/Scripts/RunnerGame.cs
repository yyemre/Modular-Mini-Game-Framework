using UnityEngine;
using Core.MiniGame;
using Core.GameStateMachine;

namespace MiniGames.EndlessRunner
{
    public class RunnerGame : MiniGameBase<RunnerState>
    {
        // [SerializeField] private RunnerController runner;
        // [SerializeField] private ObstacleSpawner spawner;
        // [SerializeField] private ScoreManager scoreManager;

        public override void RegisterStates(MiniGameStateMachine<RunnerState> machine)
        {
            machine.RegisterState(RunnerState.Loading, new RunnerLoadingState(machine, this));
            machine.RegisterState(RunnerState.Ready, new RunnerReadyState(machine, this));
            machine.RegisterState(RunnerState.Countdown, new RunnerCountdownState(machine, this));
            machine.RegisterState(RunnerState.Playing, new RunnerPlayingState(machine, this));
            machine.RegisterState(RunnerState.Crashed, new RunnerCrashedState(machine, this));
            machine.RegisterState(RunnerState.GameOver, new RunnerGameOverState(machine, this));
        }

        public override void OnGameStart()
        {
            ChangeState(RunnerState.Loading);
        }

        public void OnDeath()
        {
            ChangeState(RunnerState.Crashed);
        }

        // public RunnerController GetRunner() => runner;
        // public ObstacleSpawner GetSpawner() => spawner;
        // public ScoreManager GetScoreManager() => scoreManager;
    }

}