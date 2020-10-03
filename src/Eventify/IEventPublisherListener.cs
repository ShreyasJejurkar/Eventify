namespace Eventify
{
    /// <summary>
    /// Contract for Event listeners 
    /// </summary>
    public interface IEventPublisherListener
    {
        /// <summary>
        /// Gets executed before publishing an <see cref="Event"/>
        /// </summary>
        /// <param name="context">Context containing current <see cref="Event"/> information</param>
        void OnEventPublishing(EventPublishingContext context);

        /// <summary>
        /// Gets executed after publishing an <see cref="Event"/>
        /// </summary>
        /// <param name="context">Context containing event execution information</param>
        void OnEventPublished(EventPublishedContext context);
    }
}