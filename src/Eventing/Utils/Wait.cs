using System;
using System.Diagnostics;
using System.Threading;

namespace Eventing.Utils
{
    public static class Wait
    {
        public static void For(TimeSpan timeSpan, Func<bool> stopWaiting, Action onTimeout)
        {
            var stopwatch = Stopwatch.StartNew();
            while (!stopWaiting.Invoke())
            {
                if (stopwatch.Elapsed > timeSpan)
                {
                    onTimeout.Invoke();
                    return;
                }

                Thread.Sleep(100);
            }
        }
    }
}
