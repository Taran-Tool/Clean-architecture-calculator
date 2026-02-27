using System;

namespace Calculator.Domain.Events
{
    public interface IEventBus
    {
        void Publish<T>(T @event) where T : IEvent;
        void Subscribe<T>(Action<T> handler) where T : IEvent;
        void Unsubscribe<T>(Action<T> handler) where T : IEvent;
    }
}

