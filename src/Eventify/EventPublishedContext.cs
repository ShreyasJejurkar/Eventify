using System;
using System.Collections.Generic;

namespace Eventify
{
    /// <summary>
    /// Contains event information which gets passed to
    /// <see cref="IEventPublisherListener.OnEventPublished"/>
    /// after publishing event 
    /// </summary>
    public class EventPublishedContext : EventPublishingContext
    {
        /// <summary>
        /// Represents the does exception occurred while publishing event or not 
        /// </summary>
        public bool IsFaulted => EventException != null;

        public Exception EventException { get; private set; }

        internal EventPublishedContext(Guid eventId, string eventName, Type eventType)
            : base(eventId, eventName, eventType)
        {
        }

        internal void SetException(Exception e)
        {
            EventException = e;
        }
    }
}