using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Eventify
{
    /// <summary>
    /// A event publisher that publishes event in memory 
    /// </summary>
    public class InMemoryEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;
        private static readonly ConcurrentDictionary<Type, object> EventHandlers = new ConcurrentDictionary<Type, object>();

        public InMemoryEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task Publish(Event @event)
        {
            var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

            var handlers =
                (IEnumerable) _serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(eventHandlerType));

            foreach (var handler in handlers)
            {
                handler.GetType().GetMethod("Handle")?.Invoke(handler, new object[] {@event});
            }
            
            return Task.CompletedTask;
        }
    }
}