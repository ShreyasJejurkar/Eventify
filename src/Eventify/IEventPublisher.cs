using System.Threading.Tasks;

namespace Eventify
{
    /// <summary>
    /// Abstraction for <see cref="Event"/> publishing
    /// </summary>
    public interface IEventPublisher
    {
        Task Publish(Event @event);
    }
}