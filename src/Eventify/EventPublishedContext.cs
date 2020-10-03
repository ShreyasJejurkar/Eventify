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
        public bool IsFaulted => EventExceptionList.Count != 0;

        /// <summary>
        /// This will contain occurred exception information for further logging or processing
        /// </summary>
        public List<Exception> EventExceptionList { get; }

        /// <summary>
        /// Type of handler in which <see cref="Exception"/> has been thrown-ed
        /// </summary>
        public List<Type> ExceptionHandlerTypeList { get; }

        internal EventPublishedContext(Guid eventId, string eventName, Type eventType) : base(eventId, eventName, eventType)
        {
            EventExceptionList = new List<Exception>();
            ExceptionHandlerTypeList = new List<Type>();
        }

        internal void SetException(Exception e, Type handlerExceptionType)
        {
            EventExceptionList.Add(e);
            ExceptionHandlerTypeList.Add(handlerExceptionType);
        }
    }
}