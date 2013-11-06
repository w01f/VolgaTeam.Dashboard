using System;
using System.Threading;

namespace AdBAR
{
    /// <summary>
    /// http://stackoverflow.com/questions/2565166/net-best-way-to-execute-a-lambda-on-ui-thread-after-a-delay/2894416#2894416
    /// </summary>
    public static class UiCallbackTimer
    {
        public static void DelayExecution(TimeSpan delay, Action action)
        {
            System.Threading.Timer timer = null;
            SynchronizationContext context = SynchronizationContext.Current;

            timer = new System.Threading.Timer(
                (ignore) =>
                    {
                        timer.Dispose();

                        context.Post(ignore2 => action(), null);
                    }, null, delay, TimeSpan.FromMilliseconds(-1));
        }
    }
}