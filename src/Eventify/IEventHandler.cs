using System.Threading.Tasks;

namespace Eventify
{
    /// <summary>
    /// Represents a EventHandler for given <see cref="Event"/>
    /// </summary>
    /// <typeparam name="T">Event</typeparam>
    public interface IEventHandler<in T> where T : Event
    {
        /// <summary>
        /// Gets Called when <see cref="T"/> fired
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        Task Handle(T @event);
    }
}