using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventing
{
    public interface ISnapshot
    {
        string StreamName { get; }
        int Version { get; }
    }
}
