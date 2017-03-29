using System;
using System.Threading.Tasks;

namespace Eventing.SqlCompact.Persistence
{
    public class SqlCompactEventSourcedRepository : IEventSourcedRepository
    {
        public Task SaveAsync(IEventSourced eventSourced)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string streamName) where T : class, IEventSourced, new()
        {
            throw new NotImplementedException();
        }
    }
}
