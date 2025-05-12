using System;
using System.Collections.Generic;

namespace Core.EventSystem
{
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<Delegate>> _subscribers = new();

        public void Subscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<Delegate>();

            _subscribers[type].Add(callback);
        }

        public void Unsubscribe<T>(Action<T> callback)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
                list.Remove(callback);
        }

        public void Publish<T>(T eventData)
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var list))
            {
                foreach (var del in list)
                {
                    if (del is Action<T> action)
                        action.Invoke(eventData);
                }
            }
        }
    }
}