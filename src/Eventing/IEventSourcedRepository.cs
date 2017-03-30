using System.Threading.Tasks;

namespace Eventing
{
    public interface IEventSourcedRepository
    {
        Task<T> GetAsync<T>(string streamName) where T : class, IEventSourced, new();
        Task SaveAsync(IEventSourced eventSourced);
    }
}
