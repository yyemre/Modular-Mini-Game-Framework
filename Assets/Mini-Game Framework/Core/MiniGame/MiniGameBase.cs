using System;
using System.Linq;
using UnityEngine;

namespace Core
{
    public abstract class MiniGameBase<TState> : MonoBehaviour where TState : Enum
    {
        protected MiniGameStateMachine<TState> _stateMachine;

        protected virtual void Start()
        {
            _stateMachine = new MiniGameStateMachine<TState>();
            RegisterStates(_stateMachine);
            ValidateStates();
            OnGameStart();
        }
        protected virtual void Update()
        {
            _stateMachine?.Tick();
        }
        public abstract void OnGameStart();
        public abstract void RegisterStates(MiniGameStateMachine<TState> machine);
        protected void ValidateStates()
        {
            var allStates = Enum.GetValues(typeof(TState)).Cast<TState>();
            var missing = allStates.Where(e => !_stateMachine.HasState(e)).ToList();

            if (missing.Count > 0)
            {
                var msg = $"[MiniGameBase<{typeof(TState).Name}>] Missing state registrations: {string.Join(", ", missing)}";
                Debug.LogError(msg);
                
                throw new InvalidOperationException(msg);
            }
        }
        
        public void ChangeState(TState newState) => _stateMachine.ChangeState(newState);
        public TState CurrentState => _stateMachine.CurrentKey;
    }
}