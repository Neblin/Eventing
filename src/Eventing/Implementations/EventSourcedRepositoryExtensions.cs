using Eventing.Implementations;
using System.Threading.Tasks;

namespace Eventing
{
    public static class EventSourcedRepositoryExtensions
    {
        public static async Task<IEventSourced> GetByIdAsync<T>(this IEventSourcedRepository repo, string streamId)
             where T : class, IEventSourced, new()
        {
            var category = StreamCategoryAttribute.GetCategory(typeof(T));
            var streamName = category + Conventions.CategorySeparator + streamId;
            return await repo.GetAsync<T>(streamName);
        }
    }
}
