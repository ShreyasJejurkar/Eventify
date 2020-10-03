using System;

namespace Eventify
{
    /// <summary>
    /// Represents a Event
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// <see cref="DateTimeOffset"/> when given <see cref="Event"/> occurred.
        /// </summary>
        public DateTimeOffset EventOccurredAt { get; }

        /// <summary>
        /// Unique Event Id that represents an <see cref="Event"/>
        /// </summary>
        public Guid EventId { get; }

        protected Event()
        {
            EventId = Guid.NewGuid();
            EventOccurredAt = DateTimeOffset.UtcNow;
        }
    }
}