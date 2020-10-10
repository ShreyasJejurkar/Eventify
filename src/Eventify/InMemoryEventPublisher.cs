using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify
{
    /// <summary>
    /// A event publisher that publishes event in process/memory 
    /// </summary>
    public class InMemoryEventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IEnumerable<IEventPublisherListener> _eventPublisherListener;
        private readonly ConcurrentDictionary<Type, IEnumerable> _handlersCacheDictionary = new ConcurrentDictionary<Type, IEnumerable>();

        public InMemoryEventPublisher(IServiceProvider serviceProvider, IEnumerable<IEventPublisherListener> eventPublisherListener)
        {
            _serviceProvider = serviceProvider;
            _eventPublisherListener = eventPublisherListener;
        }

        public Task Publish(Event @event)
        {
            var eventHandlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

            var handlers = _handlersCacheDictionary.GetOrAdd(@event.GetType(), (IEnumerable)_serviceProvider.GetService(typeof(IEnumerable<>).MakeGenericType(eventHandlerType)));

            var currentEventType = @event.GetType();
            var currentEventName = currentEventType.Name;

            var eventPublishingContext =
                new EventPublishingContext(@event.EventId, currentEventName, currentEventType);

            eventPublishingContext.SetEventData(@event);

            if(_eventPublisherListener != null && _eventPublisherListener.Any())
            {
                foreach (var listeners in _eventPublisherListener)
                {
                    listeners.OnEventPublishing(eventPublishingContext);
                }
            }

            EventPublishedContext publishedContext = null;

            foreach (var handler in handlers)
            {
                publishedContext =
                    new EventPublishedContext(@event.EventId, currentEventName, currentEventType);
                try
                {
                    handler.GetType().GetMethod("Handle")?.Invoke(handler, new object[] {eventPublishingContext.GetEventData()});
                }
                catch (Exception e)
                {
                    publishedContext.SetException(e);
                    publishedContext.SetEventData(eventPublishingContext.GetEventData());
                }
            }

            if (_eventPublisherListener != null && _eventPublisherListener.Any())
            {
                foreach (var item in _eventPublisherListener)
                {
                    item.OnEventPublished(publishedContext);
                }
            }

            return Task.CompletedTask;
        }
    }
}