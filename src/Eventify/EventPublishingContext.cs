using System;

namespace Eventify
{
    /// <summary>
    /// Contains event information which gets passed to
    /// <see cref="IEventPublisherListener.OnEventPublishing"/>
    /// before publishing event 
    /// </summary>
    public class EventPublishingContext
    {
        protected object _eventData;

        /// <summary>
        /// Id of currently published <see cref="Event"/>
        /// </summary>
        public Guid EventId { get; }

        /// <summary>
        /// Name of currently publishing <see cref="Event"/>
        /// </summary>
        public string EventName { get; }

        /// <summary>
        /// Type of <see cref="Event"/> class
        /// </summary>
        public Type EventType { get; }

        internal EventPublishingContext(Guid eventId, string eventName, Type eventType)
        {
            EventId = eventId;
            EventName = eventName;
            EventType = eventType;
        }

        public void SetEventData<T>(T eventData)
        {
            _eventData = eventData;
        }

        public T GetEventData<T>()
        {
            return (T)_eventData;
        }

        public dynamic GetEventData()
        {
            return _eventData;
        }
    }
}