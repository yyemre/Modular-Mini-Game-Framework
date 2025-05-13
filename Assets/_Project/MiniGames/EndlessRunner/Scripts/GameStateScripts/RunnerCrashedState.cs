using System.Collections;
using Core.EventSystem;
using Core.GameStateMachine;
using UnityEngine;

namespace MiniGames.EndlessRunner
{
    public class RunnerCrashedState : IMiniGameState
    {
        private readonly MiniGameStateMachine<RunnerState> _fsm;
        private readonly RunnerGame _game;
        private readonly IEventBus _eventBus;

        public RunnerCrashedState(MiniGameStateMachine<RunnerState> fsm, RunnerGame game, IEventBus EventBus)
        {
            _fsm = fsm;
            _game = game;
            _eventBus = EventBus;
        }

        public void Enter()
        {
            Debug.Log("Game State: Crashed");
            _eventBus.Publish(new ScoreSavingEvent());
            _game.GetCharacter().DisableControl();
            _game.StartCoroutine(WaitAndGoToGameOver());
        }

        private IEnumerator WaitAndGoToGameOver()
        {
            yield return new WaitForSeconds(1.5f);
            _fsm.ChangeState(RunnerState.GameOver);
        }

        public void Tick() { }
        public void Exit() { }
    }

}