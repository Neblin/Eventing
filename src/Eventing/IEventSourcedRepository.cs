using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventing
{
    public interface IEventSourcedRepository
    {
        Task<T> GetAsync<T>(string streamName) where T : class, IEventSourced, new();
        Task SaveAsync(IEventSourced eventSourced);
    }
}
