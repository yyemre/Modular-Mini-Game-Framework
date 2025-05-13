using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameStateMachine
{
    public class MiniGameStateMachine<T> where T : Enum
    {
        private readonly Dictionary<T, IMiniGameState> _states = new();
        private IMiniGameState _currentState;
        
        public T CurrentKey { get; private set; }

        public void RegisterState(T key, IMiniGameState state)
        {
            _states[key] = state;
        }

        public void ChangeState(T key)
        {
            if (!_states.ContainsKey(key))
            {
                throw new InvalidOperationException($"State '{key}' not registered.");
            }
            if(_currentState == _states[key]) return;

            _currentState?.Exit();
            _currentState = _states[key];
            CurrentKey = key;
            _currentState.Enter();
        }

        public void Tick()
        {
            _currentState?.Tick();
        }

        public bool HasState(T key) => _states.ContainsKey(key);
        
        public void UnRegisterStates()
        {
            _states.Clear();
            _currentState = null;
        }

        public IEnumerable<T> RegisteredKeys => _states.Keys;
    }
}