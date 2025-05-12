using System;

namespace Core.EventSystem
{
    public interface IEventBus
    {
        void Subscribe<T>(Action<T> callback);
        void Unsubscribe<T>(Action<T> callback);
        void Publish<T>(T eventData);
    }
}