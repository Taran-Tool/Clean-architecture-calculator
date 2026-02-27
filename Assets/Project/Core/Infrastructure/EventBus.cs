using Calculator.Domain.Events;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Calculator.Infrastructure
{
    public class EventBus:IEventBus
    {
        private readonly Dictionary<Type, List<Action<IEvent>>> _handlers = new();

        public void Publish<T>(T @event) where T : IEvent
        {
            var type = typeof(T);
            if (_handlers.TryGetValue(type, out var handlers))
            {
                foreach (var handler in handlers)
                {
                    handler(@event);
                }
            }
        }

        public void Subscribe<T>(Action<T> handler) where T : IEvent
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
                _handlers[type] = new List<Action<IEvent>>();

            _handlers[type].Add(e => handler((T) e));
        }

        public void Unsubscribe<T>(Action<T> handler) where T : IEvent
        {
            var type = typeof(T);
            if (_handlers.ContainsKey(type))
            {
                // ”прощенно - в реальности нужно точное удаление
            }
        }
    }
}

